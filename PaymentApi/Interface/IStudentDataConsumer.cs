using PaymentApi.Entities;

namespace PaymentApi.Interface
{
    public interface IStudentDataConsumer
    {
        Task ConsumeStudentAsync(CancellationTokenSource cancellationToken);
    }
}
