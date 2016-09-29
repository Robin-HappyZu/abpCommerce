using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace HappyZu.CloudStore.Trip
{
    public class PaymentRecord : Entity, IHasCreationTime
    {
        /// <summary>
        /// 订单来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
