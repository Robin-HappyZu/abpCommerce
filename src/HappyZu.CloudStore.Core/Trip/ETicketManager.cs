using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Linq.Extensions;

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

        public async Task<ETicket> GetETicketBySerialNoAndHashAsync(long serailNo, string hash)
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

        public Task<IReadOnlyList<ETicket>> GetETicketsAsync(Func<IQueryable<ETicket>, IQueryable<ETicket>> query,
            IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0) request.MaxResultCount = int.MaxValue;

            var list = query == null
                ? _eTicketRepository.GetAll().OrderBy(o => o.Id).PageBy(request).ToList()
                : _eTicketRepository.Query(query).PageBy(request).ToList();

            return Task.FromResult((IReadOnlyList<ETicket>)list);
        }

        public async Task<int> GetETicketsCountByTicketOrderIdAsync(int ticketOrderId)
        {
            return await _eTicketRepository.CountAsync(eTicket => eTicket.TicketOrderId == ticketOrderId);
        }

        public Task<int> GetETicketsCountAsync(Func<IQueryable<ETicket>, IQueryable<ETicket>> query)
        {
            var count = query == null ? _eTicketRepository.Count() : _eTicketRepository.Query(query).Count();
            return Task.FromResult(count);
        }
    }
}