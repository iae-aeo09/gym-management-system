
USE GymManagementDB;

CREATE TABLE Members (
    MemberID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    [Plan]   NVARCHAR(20) NOT NULL,       -- Monthly, Quarterly, Semi-Annual, Annual
    MembershipFee DECIMAL(10,2) NOT NULL,
    JoinDate DATE NOT NULL,
    ExpiryDate DATE NOT NULL,
    Status NVARCHAR(20) DEFAULT 'Active',  -- Active, Expired
    IsPaid BIT DEFAULT 0,
    IsArchived BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    MemberID INT FOREIGN KEY REFERENCES Members(MemberID),
    MemberName NVARCHAR(100),
    Amount DECIMAL(10,2) NOT NULL,
    PaymentMethod NVARCHAR(30),  -- Cash, GCash, PayPal, Bank Transfer
    PaymentDate DATE NOT NULL,
    ReferenceNo NVARCHAR(20),
    Status NVARCHAR(20) DEFAULT 'Paid',
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) DEFAULT 'Admin',
    CreatedAt DATETIME DEFAULT GETDATE()
);

INSERT INTO Users (Username, PasswordHash, Role)
VALUES ('admin', 'admin123', 'Admin');

CREATE PROCEDURE sp_ArchiveMember @MemberID INT AS
BEGIN
    UPDATE Members SET IsArchived = 1, Status = 'Archived'
    WHERE MemberID = @MemberID;
END;


CREATE PROCEDURE sp_GetExpiringSoon AS
BEGIN
    SELECT MemberID, FullName, Email, ExpiryDate,
           DATEDIFF(DAY, GETDATE(), ExpiryDate) AS DaysLeft
    FROM Members
    WHERE IsArchived = 0
      AND DATEDIFF(DAY, GETDATE(), ExpiryDate) BETWEEN 0 AND 7;
END;


CREATE PROCEDURE sp_RecordPayment
    @MemberID INT, @Amount DECIMAL(10,2), @Method NVARCHAR(30),
    @PayDate DATE, @RefNo NVARCHAR(20)
AS
BEGIN
    INSERT INTO Payments (MemberID, MemberName, Amount, PaymentMethod, PaymentDate, ReferenceNo, Status)
    SELECT @MemberID, FullName, @Amount, @Method, @PayDate, @RefNo, 'Paid'
    FROM Members WHERE MemberID = @MemberID;

    UPDATE Members SET IsPaid = 1, Status = 'Active' WHERE MemberID = @MemberID;
END;

-- Feature upgrades
IF COL_LENGTH('Members', 'IsFrozen') IS NULL
    ALTER TABLE Members ADD IsFrozen BIT NOT NULL CONSTRAINT DF_Members_IsFrozen DEFAULT(0);

IF COL_LENGTH('Members', 'FrozenFrom') IS NULL
    ALTER TABLE Members ADD FrozenFrom DATE NULL;

IF COL_LENGTH('Members', 'FrozenUntil') IS NULL
    ALTER TABLE Members ADD FrozenUntil DATE NULL;

IF OBJECT_ID('dbo.ReminderLogs', 'U') IS NULL
BEGIN
    CREATE TABLE ReminderLogs (
        ReminderID INT PRIMARY KEY IDENTITY(1,1),
        MemberID INT NOT NULL,
        ReminderType NVARCHAR(30) NOT NULL,
        SentAt DATETIME NOT NULL DEFAULT GETDATE(),
        Channel NVARCHAR(20) NULL,
        Notes NVARCHAR(200) NULL
    );
END;

GO
CREATE OR ALTER PROCEDURE sp_FreezeMembership
    @MemberID INT,
    @Days INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Members
        SET IsFrozen = 1,
            FrozenFrom = CAST(GETDATE() AS date),
            FrozenUntil = DATEADD(day, @Days, CAST(GETDATE() AS date)),
            ExpiryDate = DATEADD(day, @Days, ExpiryDate),
            Status = 'Frozen'
        WHERE MemberID = @MemberID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;

GO
CREATE OR ALTER PROCEDURE sp_RecordPaymentWithTx
    @MemberID INT,
    @Amount DECIMAL(10,2),
    @Method NVARCHAR(30),
    @PayDate DATE,
    @RefNo NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Payments (MemberID, MemberName, Amount, PaymentMethod, PaymentDate, ReferenceNo, Status)
        SELECT @MemberID, FullName, @Amount, @Method, @PayDate, @RefNo, 'Paid'
        FROM Members
        WHERE MemberID = @MemberID;

        UPDATE Members
        SET IsPaid = 1,
            Status = CASE
                WHEN IsFrozen = 1 THEN 'Frozen'
                WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired'
                ELSE 'Active'
            END
        WHERE MemberID = @MemberID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;