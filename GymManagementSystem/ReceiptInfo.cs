using System;

namespace GymManagementSystem
{
    public class ReceiptInfo
    {
        public string ReferenceNo { get; set; }
        public string MemberName { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
        public string Plan { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Benefits { get; set; }
    }
}
