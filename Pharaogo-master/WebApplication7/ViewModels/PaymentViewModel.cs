namespace WebApplication7.ViewModels
{
    public class PaymentViewModel
    {   
        public int NumberOfGuests { get; set; }
        public int? TotalDayes { get; set; }
        public string PormotionDiscount { get; set; }
        public decimal TotalAmount { get; set; } // Total amount for the booking
        public decimal TotalAmountAfterDiss { get; set; } // Total amount for the booking
        public string PaymentCode { get; set; } = string.Empty; // Fake payment code
    }
}
