using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using HappyZu.CloudStore.Entities;

namespace HappyZu.CloudStore.Trip
{
    /// <summary>
    /// 旅游订单
    /// </summary>
    [Table("Trip_TravelOrder")]
    public class TravelOrder:Entity,ISoftDelete,ICreationAudited
    {
        /// <summary>
        /// 出发日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [StringLength(128)]
        public string OrderNo { get; set; }

        public int UsedPoint { get; set; }

        /// <summary>
        /// 成人数量
        /// </summary>
        public int AdultCount { get; set; }

        /// <summary>
        /// 成人报价
        /// </summary>
        public double AdultPrice { get; set; }

        /// <summary>
        /// 儿童数量
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// 儿童报价
        /// </summary>
        public double ChildPrice { get; set; }

        /// <summary>
        /// 老人数量
        /// </summary>
        public int OldManCount { get; set; }

        /// <summary>
        /// 老人报价
        /// </summary>
        public double OldManPrice { get; set; }

        /// <summary>
        /// 单房差
        /// </summary>
        public double SingleSupplement { get; set; }

        /// <summary>
        /// 保险费
        /// </summary>
        public double InsurancePremium { get; set; }

        /// <summary>
        /// 订单总价
        /// </summary>
        public double TotalAmount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public double PaidAmount { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [StringLength(50)]
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(50)]
        public string Mobile { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        public string Remark { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long? CreatorUserId { get; set; }
    }

    
}
