# GymManagementSystem SQL Code Documentation

This document explains how the SQL code in the system works, where it is used, and what each query does.

---

## 1) Database Connection and Schema Setup

### File
- `DBConnection.cs`

### Connection Source
- Reads connection string from `App.config` key: `GymManagementDb`.
- Falls back to default local SQL Server connection if key is missing.

### SQL: Feature Schema Initialization
Used by `DBConnection.EnsureFeatureSchema()`.

```sql
IF COL_LENGTH('Members', 'IsFrozen') IS NULL
    ALTER TABLE Members ADD IsFrozen BIT NOT NULL CONSTRAINT DF_Members_IsFrozen DEFAULT(0);

IF COL_LENGTH('Members', 'FrozenFrom') IS NULL
    ALTER TABLE Members ADD FrozenFrom DATE NULL;

IF COL_LENGTH('Members', 'FrozenUntil') IS NULL
    ALTER TABLE Members ADD FrozenUntil DATE NULL;

IF COL_LENGTH('Members', 'FreezeUsed') IS NULL
    ALTER TABLE Members ADD FreezeUsed BIT NOT NULL CONSTRAINT DF_Members_FreezeUsed DEFAULT(0);

IF OBJECT_ID('dbo.ReminderLogs', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ReminderLogs(
        ReminderID INT IDENTITY(1,1) PRIMARY KEY,
        MemberID INT NOT NULL,
        ReminderType NVARCHAR(30) NOT NULL,
        SentAt DATETIME NOT NULL DEFAULT(GETDATE()),
        Channel NVARCHAR(20) NULL,
        Notes NVARCHAR(200) NULL
    );
END;
```

**Purpose**
- Ensures freeze-related columns exist in `Members`.
- Creates `ReminderLogs` table if missing.

### SQL: Auto Unfreeze
Used by `DBConnection.AutoUnfreezeExpiredMembers()`.

```sql
UPDATE Members
SET IsFrozen = 0,
    FrozenFrom = NULL,
    FrozenUntil = NULL,
    Status = CASE
        WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired'
        ELSE 'Active'
    END
WHERE IsArchived = 0
  AND IsFrozen = 1
  AND FrozenUntil IS NOT NULL
  AND CAST(FrozenUntil AS date) <= CAST(GETDATE() AS date);
```

**Purpose**
- Automatically unfreezes members whose freeze period has ended.

---

## 2) Login Module

### File
- `Form1.cs`

### SQL: User Authentication

```sql
SELECT COUNT(*)
FROM Users
WHERE Username=@u AND PasswordHash=@p;
```

**Purpose**
- Validates login credentials.
- If count > 0, user is allowed into `Dashboard`.

---

## 3) Dashboard Module

### File
- `Dashboard.cs`

### SQL: Total Members

```sql
SELECT COUNT(*) FROM Members WHERE IsArchived=0;
```

### SQL: Active Members

```sql
SELECT COUNT(*)
FROM Members
WHERE IsArchived=0
  AND CAST(ExpiryDate AS date) >= CAST(GETDATE() AS date);
```

### SQL: Expired Members

```sql
SELECT COUNT(*)
FROM Members
WHERE IsArchived=0
  AND CAST(ExpiryDate AS date) < CAST(GETDATE() AS date);
```

### SQL: Revenue by Date Filter

```sql
SELECT ISNULL(SUM(Amount),0)
FROM Payments
WHERE <dateFilter>;
```

`<dateFilter>` depends on selected period:
- Today
- Last 7 Days
- This Month
- All Time

### SQL: Recent Payments Grid (with Member-based Status + Expiry)

```sql
SELECT TOP 5
    p.ReferenceNo,
    p.MemberName,
    p.Amount,
    p.PaymentMethod,
    p.PaymentDate,
    m.ExpiryDate,
    CASE
        WHEN m.MemberID IS NULL THEN ISNULL(p.Status, 'Paid')
        WHEN m.IsFrozen = 1 THEN 'Frozen'
        WHEN CAST(m.ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired - Inactive'
        WHEN DATEDIFF(DAY, CAST(GETDATE() AS date), CAST(m.ExpiryDate AS date)) BETWEEN 0 AND 7
            THEN CASE WHEN m.IsPaid = 1 THEN 'Expiring - Paid' ELSE 'Expiring - Unpaid' END
        ELSE CASE WHEN m.IsPaid = 1 THEN 'Active - Paid' ELSE 'Active - Unpaid' END
    END AS Status
FROM Payments p
LEFT JOIN Members m ON p.MemberID = m.MemberID
WHERE <dateFilter>
ORDER BY p.PaymentID DESC;
```

### SQL: Expiring Membership Alert

```sql
SELECT FullName, Email, ExpiryDate,
       DATEDIFF(DAY, GETDATE(), ExpiryDate) AS DaysLeft
FROM Members
WHERE IsArchived=0
  AND IsFrozen=0
  AND DATEDIFF(DAY, GETDATE(), ExpiryDate) BETWEEN 0 AND 7;
```

### SQL: Monthly Analytics Summary

```sql
SELECT
    (SELECT COUNT(*) FROM Members
      WHERE IsArchived=0
        AND YEAR(JoinDate)=YEAR(GETDATE())
        AND MONTH(JoinDate)=MONTH(GETDATE())) AS NewMembers,
    (SELECT COUNT(*) FROM Payments
      WHERE <dateFilter>) AS PaymentsThisMonth,
    (SELECT ISNULL(SUM(Amount),0) FROM Payments
      WHERE <dateFilter>) AS RevenueThisMonth,
    (SELECT COUNT(*) FROM Members WHERE IsArchived=0 AND IsFrozen=1) AS FrozenMembers;
```

### SQL: Reminder Candidate List

```sql
SELECT m.MemberID, m.FullName, m.Email, m.ExpiryDate,
       DATEDIFF(DAY, CAST(GETDATE() AS date), CAST(m.ExpiryDate AS date)) AS DaysLeft
FROM Members m
WHERE m.IsArchived=0
  AND m.IsFrozen=0
  AND DATEDIFF(DAY, CAST(GETDATE() AS date), CAST(m.ExpiryDate AS date)) BETWEEN 0 AND 7
  AND NOT EXISTS (
      SELECT 1 FROM ReminderLogs r
      WHERE r.MemberID = m.MemberID
        AND r.ReminderType = 'Renewal'
        AND CAST(r.SentAt AS date) = CAST(GETDATE() AS date)
  );
```

### SQL: Reminder Logging

```sql
INSERT INTO ReminderLogs (MemberID, ReminderType, Channel, Notes)
VALUES (@id,'Renewal','Email',@notes);
```

---

## 4) Members Module

### File
- `Member.cs`

### SQL: Members Grid with Combined Status and Payment Display

```sql
SELECT MemberID, FullName, Email, Phone, [Plan], 
       MembershipFee, JoinDate, ExpiryDate, IsFrozen, FrozenFrom, FrozenUntil, FreezeUsed,
       CASE
           WHEN IsFrozen = 1 THEN 'Frozen'
           WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired - Inactive'
           WHEN DATEDIFF(DAY, CAST(GETDATE() AS date), CAST(ExpiryDate AS date)) BETWEEN 0 AND 7
               THEN CASE WHEN IsPaid = 1 THEN 'Expiring - Paid' ELSE 'Expiring - Unpaid' END
           ELSE CASE WHEN IsPaid = 1 THEN 'Active - Paid' ELSE 'Active - Unpaid' END
       END AS [Status],
       CASE
           WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Unpaid'
           WHEN IsPaid=1 THEN 'Paid'
           ELSE 'Unpaid'
       END AS Payment
FROM Members
WHERE IsArchived=0
  AND (FullName LIKE @s OR Email LIKE @s)
ORDER BY MemberID DESC;
```

### SQL: Archive Member

```sql
UPDATE Members
SET IsArchived=1, Status='Archived'
WHERE MemberID=@id;
```

### SQL: Freeze Member (7 days, one-time)

```sql
UPDATE Members
SET IsFrozen = 1,
    FreezeUsed = 1,
    FrozenFrom = CAST(GETDATE() AS date),
    FrozenUntil = DATEADD(day, @days, CAST(GETDATE() AS date)),
    ExpiryDate = DATEADD(day, @days, ExpiryDate),
    Status = 'Frozen'
WHERE MemberID = @id;
```

### SQL: Manual Unfreeze

```sql
UPDATE Members
SET IsFrozen = 0,
    FrozenFrom = NULL,
    FrozenUntil = NULL,
    Status = CASE
        WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired'
        ELSE 'Active'
    END
WHERE MemberID = @id;
```

### SQL: Reminder Candidate + Log
- Same logic as Dashboard reminder section.

---

## 5) Add / Edit Member Module

### File
- `AddEditMember.cs`

### SQL: Load Existing Member

```sql
SELECT * FROM Members WHERE MemberID=@id;
```

### SQL: Insert New Member

```sql
INSERT INTO Members (FullName,Email,Phone,[Plan],MembershipFee,JoinDate,ExpiryDate,Status,IsPaid,IsArchived)
VALUES (@n,@e,@ph,@pl,@f,@j,@ex,@status,0,0);
```

### SQL: Update Existing Member

```sql
UPDATE Members
SET FullName=@n,Email=@e,Phone=@ph,[Plan]=@pl,
    MembershipFee=@f,JoinDate=@j,ExpiryDate=@ex,Status=@status
WHERE MemberID=@id;
```

**Note**
- Renewal flow sets start date to current date in UI before save.

---

## 6) Payments Module

### File
- `Payments.cs`

### SQL: Load Members for Payment Form

```sql
SELECT MemberID, FullName, [Plan], MembershipFee, Email, ExpiryDate, IsPaid
FROM Members
WHERE IsArchived=0;
```

### SQL: Payment History Grid

```sql
SELECT p.ReferenceNo, p.MemberName, p.Amount,
       p.PaymentMethod, p.PaymentDate, m.ExpiryDate, m.[Plan], p.Status
FROM Payments p
LEFT JOIN Members m ON p.MemberID = m.MemberID
ORDER BY p.PaymentID DESC;
```

### SQL: Insert Payment Transaction

```sql
INSERT INTO Payments (MemberID, MemberName, Amount, PaymentMethod, PaymentDate, ReferenceNo, Status)
VALUES (@mid, @mn, @amt, @mth, @dt, @ref, 'Paid');
```

### SQL: Update Member on Successful Payment

```sql
UPDATE Members
SET IsPaid=1,
    ExpiryDate=@newExpiry,
    Status = CASE 
        WHEN IsFrozen=1 THEN 'Frozen'
        WHEN CAST(@newExpiry AS date) < CAST(GETDATE() AS date) THEN 'Expired'
        WHEN DATEDIFF(DAY, CAST(GETDATE() AS date), CAST(@newExpiry AS date)) BETWEEN 0 AND 7 THEN 'Expiring'
        ELSE 'Active'
    END
WHERE MemberID=@id;
```

**Current expiry rule**
- `newExpiry = payDate + planMonths`
- This supports backdated demo scenarios correctly.

---

## 7) Archive Module

### File
- `Archive.cs`

### SQL: Load Archived Members

```sql
SELECT MemberID, FullName, Email, Phone, [Plan], MembershipFee, JoinDate, ExpiryDate
FROM Members
WHERE IsArchived = 1;
```

### SQL: Restore Archived Member

```sql
UPDATE Members
SET IsArchived=0,
    Status = CASE 
        WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired'
        ELSE 'Active'
    END
WHERE MemberID=@id;
```

---

## 8) Receipt / PDF / Email

### Files
- `ReceiptDialog.cs`
- `ReceiptPdfExporter.cs`
- `EmailHelper.cs`

### SQL Involved
- No direct SQL here.
- Uses payment/member values already loaded from payment history/member selection.

**Key behavior**
- Receipt includes: `ReferenceNo`, `MemberName`, `Amount`, `PaymentMethod`, `PaymentDate`, `Status`, `Plan`, `ExpiryDate`, `Benefits`.
- PDF is generated programmatically.
- Email sending supports fake mode and real SMTP mode.

---

## 9) SQL Parameters and Security Notes

### Good practice used
- Parameterized SQL (`@id`, `@s`, `@u`, etc.) to reduce SQL injection risk.

### Important improvement opportunities
- Login currently compares plain input with `PasswordHash` directly (should use real hash verification).
- Consider adding FK from `ReminderLogs.MemberID` to `Members.MemberID` in DB for stronger integrity.

---

## 10) SQL Object Summary

### Core Tables Used
- `Users`
- `Members`
- `Payments`
- `ReminderLogs`

### Main FK in current database
- `Payments.MemberID -> Members.MemberID`

