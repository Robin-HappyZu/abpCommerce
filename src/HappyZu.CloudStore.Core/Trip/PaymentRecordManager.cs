using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace HappyZu.CloudStore.Trip
{
    public class PaymentRecordManager : IDomainService
    {
        private readonly IRepository<PaymentRecord> _paymentRecordRepository;

        public PaymentRecordManager(IRepository<PaymentRecord> paymentRecordRepository)
        {
            _paymentRecordRepository = paymentRecordRepository;
        }

        public async Task AddPaymentRecordAsync(PaymentRecord payment)
        {
            await _paymentRecordRepository.InsertAsync(payment);
        }
    }
}
