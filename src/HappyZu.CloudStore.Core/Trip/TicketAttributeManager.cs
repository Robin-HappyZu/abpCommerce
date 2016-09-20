using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace HappyZu.CloudStore.Trip
{
    public class TicketAttributeManager : IDomainService
    {
        private readonly IRepository<TicketAttribute> _ticketAttributeRepository;

        public TicketAttributeManager(IRepository<TicketAttribute> ticketAttributeRepository)
        {
            _ticketAttributeRepository = ticketAttributeRepository;
        }

        public async Task AddTicketAttributeAsync(TicketAttribute attr)
        {
            await _ticketAttributeRepository.InsertAsync(attr);
        }

        public async Task UpdateTicketAttributeAsync(TicketAttribute attr)
        {
            await _ticketAttributeRepository.UpdateAsync(attr);
        }

        public async Task RemoveTicketAttributeAsync(int ticketAttributeId)
        {
            await _ticketAttributeRepository.DeleteAsync(ticketAttributeId);
        }

        public async Task<TicketAttribute> GetTicketAttributeByIdAsync(int ticketAttributeId)
        {
            return await _ticketAttributeRepository.GetAsync(ticketAttributeId);
        }

        public async Task<IList<TicketAttribute>> GetSubTicketAttributesByParentIdAsync(int parentId)
        {
            return await _ticketAttributeRepository.GetAllListAsync(attr => attr.ParentId == parentId);
        }
    }
}
