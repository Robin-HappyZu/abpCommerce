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

        public async Task<int> GetTicketOrdersCountAsync()
        {
            return await _ticketOrderRepository.CountAsync();
        }

        public IList<TicketOrder> GetPagedTicketOrders(IPagedResultRequest request)
        {
            return _ticketOrderRepository.GetAll().PageBy(request).ToList();
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
