using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace HappyZu.CloudStore.Trip
{
    public class TicketManager : IDomainService
    {
        private readonly IRepository<Ticket> _ticketRepository;

        public TicketManager(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        #region 门票

        public async Task AddTicketAsync(Ticket ticket)
        {
            await _ticketRepository.InsertAsync(ticket);
        }

        public async Task UpdateTicketAysnc(Ticket ticket)
        {
            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task RemoveTicketAsync(int ticketId)
        {
            await _ticketRepository.DeleteAsync(ticketId);
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _ticketRepository.GetAsync(ticketId);
        }

        public async Task<IList<Ticket>> GetTicketsByDestId(int destId)
        {
            return await _ticketRepository.GetAllListAsync(ticket => ticket.DestId == destId);
        }

        #endregion
    }
}
