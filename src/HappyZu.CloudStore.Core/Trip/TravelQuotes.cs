using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace HappyZu.CloudStore.Trip
{
    /// <summary>
    /// 线路报价单
    /// </summary>
    [Table("Trip_TravelQuotes")]
    public class TravelQuotes:Entity, ISoftDelete, IAudited
    {
        /// <summary>
        /// 旅游线路Id
        /// </summary>
        public int TravelId { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 周
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// 成人报价
        /// </summary>
        public Quote Quote { get; set; }

        /// <summary>
        /// 儿童报价
        /// </summary>
        public Quote ChildQuote { get; set; }

        /// <summary>
        /// 老人报价
        /// </summary>
        public Quote OldManQuote { get; set; }

        /// <summary>
        /// 单房差
        /// </summary>
        public decimal SingleSupplement { get; set; }

        /// <summary>
        /// 销售量
        /// </summary>
        public int Sales { get; set; }

        /// <summary>
        /// 显示
        /// </summary>
        public bool IsDisplay { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
