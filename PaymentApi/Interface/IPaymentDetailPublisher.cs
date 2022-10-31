using PaymentApi.Entities;

namespace PaymentApi.Interface
{
    public interface IPaymentDetailPublisher
    {
        Task<bool> ProducePaymentDataAsync(PaymentDetails details);
        
    }
}
