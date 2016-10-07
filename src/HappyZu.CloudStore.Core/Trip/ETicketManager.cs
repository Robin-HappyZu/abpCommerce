using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace HappyZu.CloudStore.Trip
{
    public class ETicketManager : IDomainService
    {
        private readonly IRepository<ETicket> _eTicketRepository;

        public ETicketManager(IRepository<ETicket> eTicketRepository)
        {
            _eTicketRepository = eTicketRepository;
        }

        public async Task InsertETicketAsync(ETicket eTicket)
        {
            await _eTicketRepository.InsertAsync(eTicket);
        }

        public async Task UpdateETicketAsync(ETicket eTicket)
        {
            await _eTicketRepository.UpdateAsync(eTicket);
        }

        public async Task<ETicket> GetETicketBySerialNoAndHashAsync(int serailNo, string hash)
        {
            return await _eTicketRepository.FirstOrDefaultAsync(
                        eTicket => eTicket.SerialNo == serailNo && eTicket.Hash == hash);
        }

        public async Task<ETicket> GetETicketByIdAsync(int eTicketId)
        {
            return await _eTicketRepository.GetAsync(eTicketId);
        }

        public async Task<IList<ETicket>> GetETicketsByTicketOrderIdAsync(int ticketOrderId)
        {
            return await _eTicketRepository.GetAllListAsync(eTicket => eTicket.TicketOrderId == ticketOrderId);
        }
    }
}