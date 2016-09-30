using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace HappyZu.CloudStore.Trip
{
    public class TicketTypeManager:IDomainService
    {
        private readonly IRepository<TicketType> _ticketRepository;
        public TicketTypeManager(IRepository<TicketType> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketType> GetAsync(EntityDto entity)
        {
            return await _ticketRepository.GetAsync(entity.Id);
        }

        public async Task RemoveAsync(EntityDto entity)
        {
            await _ticketRepository.DeleteAsync(x => x.Id == entity.Id);
        }

        public async Task<IList<TicketType>> GetListAsync(int destId)
        {
            return await _ticketRepository.GetAllListAsync(x => x.DestId == destId);
        }

        public async Task AddAsync(TicketType entity)
        {
            await _ticketRepository.InsertAsync(entity);
        }

        public async Task<int> AddAndGetIdAsync(TicketType entity)
        {
            return await _ticketRepository.InsertAndGetIdAsync(entity);
        }
    }
}
