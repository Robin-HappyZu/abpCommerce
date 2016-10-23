using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Entities;

namespace HappyZu.CloudStore.Trip.Dto
{
    public class GetPagedTicketOrdersInput : IPagedResultRequest
    {
        public OrderStatus? OrderStatus { get; set; }

        public long UserId { get; set; }

        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
    }
}
