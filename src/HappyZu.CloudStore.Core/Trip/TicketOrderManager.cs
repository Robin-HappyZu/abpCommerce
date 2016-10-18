using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Linq.Extensions;

namespace HappyZu.CloudStore.Trip
{
    public class TicketOrderManager : IDomainService
    {
        private readonly IRepository<TicketOrder> _ticketOrderRepository;
        private readonly IRepository<TicketOrderItem> _ticketOrderItemRepository; 

        public TicketOrderManager(IRepository<TicketOrder> ticketOrderRepository, IRepository<TicketOrderItem> ticketOrderItemRepository)
        {
            _ticketOrderRepository = ticketOrderRepository;
            _ticketOrderItemRepository = ticketOrderItemRepository;
        }

        #region 门票订单

        public async Task AddTicketOrderAsync(TicketOrder ticketOrder)
        {
            await _ticketOrderRepository.InsertAsync(ticketOrder);
        }

        public async Task<int> AddTicketOrderAndGetIdAsync(TicketOrder ticketOrder)
        {
            return await _ticketOrderRepository.InsertAndGetIdAsync(ticketOrder);
        }

        public async Task UpdateTicketOrderAsync(TicketOrder ticketOrder)
        {
            await _ticketOrderRepository.UpdateAsync(ticketOrder);
        }

        public async Task RemoveTicketOrderAsync(int ticketOrderId)
        {
            await _ticketOrderRepository.DeleteAsync(ticketOrderId);
        }

        public async Task<TicketOrder> GetTicketOrderByIdAsync(int ticketOrderId)
        {
            return await _ticketOrderRepository.GetAsync(ticketOrderId);
        }

        public Task<int> GetTicketOrdersCountAsync(Func<IQueryable<TicketOrder>, IQueryable<TicketOrder>> query)
        {
            var count = query == null ? _ticketOrderRepository.Count() : _ticketOrderRepository.Query(query).Count();
            return Task.FromResult(count);
        }

        public IList<TicketOrder> GetPagedTicketOrders(IPagedResultRequest request)
        {
            return _ticketOrderRepository.GetAll().PageBy(request).ToList();
        }

        public Task<IReadOnlyList<TicketOrder>> GetTicketOrdersAsync(Func<IQueryable<TicketOrder>, IQueryable<TicketOrder>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0) request.MaxResultCount = int.MaxValue;

            var list = query == null
                ? _ticketOrderRepository.GetAll().OrderBy(o => o.Id).PageBy(request).ToList()
                : _ticketOrderRepository.Query(query).PageBy(request).ToList();

            return Task.FromResult((IReadOnlyList<TicketOrder>) list);
        }

        #endregion

        #region 门票订单项

        public async Task AddTicketOrderDetailsAsync(List<TicketOrderItem> items)
        {
            foreach (var item in items)
            {
                await _ticketOrderItemRepository.InsertAsync(item);
            }
        }

        public async Task<IList<TicketOrderItem>> GetTicketOrderDetailsByTicketOrderIdAsync(int ticketOrderId)
        {
            return await _ticketOrderItemRepository.GetAllListAsync(item => item.TicketOrderId == ticketOrderId);
        }

        #endregion
    }
}
