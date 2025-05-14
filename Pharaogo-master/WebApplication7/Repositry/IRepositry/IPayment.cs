using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Repositry.IRepositry
{
    public interface IPayment
    {
        Payment GetPaymentById(int id);
        PaymentViewModel GetPaymentViewModel(int id);
        void CreatePayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(int id);
        void SaveChanges();
    }
}
