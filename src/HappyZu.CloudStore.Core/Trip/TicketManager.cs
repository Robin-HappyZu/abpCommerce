using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Linq.Extensions;
using Castle.DynamicProxy.Generators.Emitters;

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

        public async Task<int> GetDestTicketsCountAsync(int destId = 0)
        {
            if (destId == 0)
            {
                return await _ticketRepository.CountAsync();
            }

            return await _ticketRepository.CountAsync(ticket => ticket.DestId == destId);
        }

        public IList<Ticket> GetPagedTickets(int destId, IPagedResultRequest request)
        {
            var query = _ticketRepository.GetAll();

            if (destId > 0)
            {
                query = query.Where(ticket => ticket.DestId == destId);
            }

            return query.PageBy(request).ToList();
        }

        public async Task<IList<Ticket>> GetTicketsByDestIdAsync(int destId = 0)
        {
            if (destId == 0)
            {
                return await _ticketRepository.GetAllListAsync();
            }

            return await _ticketRepository.GetAllListAsync(ticket => ticket.DestId == destId);
        }

        #endregion
    }
}
