using WebApplication7.Data;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;
using AutoMapper;

namespace WebApplication7.Repositry
{
    /// <summary>
    /// Repository for payment operations
    /// </summary>
    public class Payment_Repo : IPayment
    {
        private readonly DepiContext _context;
        private readonly IMapper _mapper;

        public Payment_Repo(DepiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Payment GetPaymentById(int id)
        {
            return _context.Payment.Find(id);
        }

        public PaymentViewModel GetPaymentViewModel(int id)
        {
            var payment = _context.Payment.Find(id);
            if (payment == null)
            {
                return null;
            }

            // Map the payment entity to view model using AutoMapper
            var paymentViewModel = _mapper.Map<PaymentViewModel>(payment);
            
            return paymentViewModel;
        }

        public void CreatePayment(Payment payment)
        {
            _context.Payment.Add(payment);
            SaveChanges();
        }

        public void UpdatePayment(Payment payment)
        {
            _context.Payment.Update(payment);
            SaveChanges();
        }

        public void DeletePayment(int id)
        {
            var payment = _context.Payment.Find(id);
            if (payment != null)
            {
                _context.Payment.Remove(payment);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
