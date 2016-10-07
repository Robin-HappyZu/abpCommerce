using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMap(typeof(ETicket))]
    public class ETicketDto : EntityDto
    {
        public int TicketId { get; set; }
        // 门票订单Id
        public int TicketOrderId { get; set; }
        // 订单详情Id
        public int TicketorderItemId { get; set; }
        // 电子票序列号 全局必须唯一
        public long SerialNo { get; set; }
        // 门票Hash字串 防止假冒门票
        public string Hash { get; set; }
        // 电子票描述
        public string Description { get; set; }
        // 创建时间
        public DateTime CreateOn { get; set; }
        // 是否已经检票
        public bool IsChecked { get; set; }
        // 检票时间
        public DateTime CheckedOn { get; set; }
    }
}
