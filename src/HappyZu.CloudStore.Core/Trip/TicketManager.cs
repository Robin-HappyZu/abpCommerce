using System;
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
        private readonly IRepository<TicketAttributeRecord> _ticketAttributeRecordRepository; 

        public TicketManager(IRepository<Ticket> ticketRepository, IRepository<TicketAttributeRecord> ticketAttributeRecordRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketAttributeRecordRepository = ticketAttributeRecordRepository;
        }
        #region 门票

        public async Task AddTicketAsync(Ticket ticket)
        {
            await _ticketRepository.InsertAsync(ticket);
        }

        public async Task<int> AddTicketAndGetIdAsync(Ticket ticket)
        {
            return await _ticketRepository.InsertAndGetIdAsync(ticket);
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

        public Task<IReadOnlyList<Ticket>> QuerysListAsync(Func<IQueryable<Ticket>, IQueryable<Ticket>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _ticketRepository.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _ticketRepository.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<Ticket>)list);
        }

        public Task<IReadOnlyList<Ticket>> QuerysListAsync(IPagedResultRequest request)
        {
            return QuerysListAsync(null, request);
        }

        public Task<int> QueryCountAsync(Func<IQueryable<Ticket>, IQueryable<Ticket>> query)
        {
            var count = query != null ?
                    _ticketRepository.Query(query).Count() :
                    _ticketRepository.Count();

            return Task.FromResult(count);
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

        #region 门票标签

        public async Task AttachTicketAttributeRecordAsync(List<TicketAttributeRecord> records)
        {
            foreach (var record in records)
            {
                await _ticketAttributeRecordRepository.InsertAsync(record);
            }
        }

        public async Task DetachTicketAttributeRecordAsync(List<TicketAttributeRecord> records)
        {
            foreach (var record in records)
            {
                await _ticketAttributeRecordRepository.DeleteAsync(record.Id);
            }
        }

        public async Task<List<TicketAttributeRecord>> GetAttributeRecordsByTicketIdAsync(int ticketId)
        {
            return await _ticketAttributeRecordRepository.GetAllListAsync(record => record.TicketId == ticketId);
        }

        #endregion
    }
}
