using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;
using HappyZu.CloudStore.Agents.Dto;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Users;

namespace HappyZu.CloudStore.Agents.Events
{
    public class CreateRebateEventHandler : IEventHandler<CreateRebateEventData>, ITransientDependency
    {
        private readonly ITicketAppService _ticketAppService;
        private readonly IUserAppService _userAppService;
        private readonly IRebateAppService _rebateAppService;
        public ILogger Logger { get; set; }

        public CreateRebateEventHandler(ITicketAppService ticketAppService, IUserAppService userAppService, IRebateAppService rebateAppService)
        {
            _ticketAppService = ticketAppService;
            _userAppService = userAppService;
            _rebateAppService = rebateAppService;
            Logger = NullLogger.Instance;
        }

        public void HandleEvent(CreateRebateEventData eventData)
        {
            if (eventData.OrderType=="Ticket")
            {
                TicketRebate(eventData);
            }
        }

        private async void TicketRebate(CreateRebateEventData eventData)
        {
            var order =await _ticketAppService.GetTicketOrderByIdAsync(eventData.OrderId);
            var user= await _userAppService.GetUserByIdAsync(order.CustomerId);
            var tickets= await _ticketAppService.GetTicketOrderItemsByTicketOrderIdAsync(order.Id);

            var agentAmount = tickets.Where(x=>x.AgentPrice>0).Sum(x => (x.UnitPrice-x.AgentPrice)*x.Quantity);

            var input = new CreateRecordInput();
            var recordDto = new RebateDto
            {
                AgentId=eventData.AgentId,
                ExpectedRebateDate=DateTime.Now.AddMonths(1),
                OrderAmount=order.TotalAmount,
                OrderNo=order.OrderNo,
                OrderType="Ticket",
                PaidTime=DateTime.Now,
                UserName=user.Name,
                RebateAmount= agentAmount,
                RebateDate = DateTime.Now.AddMonths(1)
            };
            input.Rebate = recordDto;

            var result= await _rebateAppService.CreateRecordAsync(input);
            if (!result.Status)
            {
                //添加失败写入日志
                Logger.Error($"代理商返利失败：OrderType:{eventData.OrderType},OrderId:{eventData.OrderId},AgentId:{eventData.AgentId}");
            }
        }
    }
}
