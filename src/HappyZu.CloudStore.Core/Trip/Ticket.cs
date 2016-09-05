using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip
{
    /// <summary>
    /// 门票
    /// </summary>
    [Table("Trip_Ticket")]
    public class Ticket
    {
        /// <summary>
        /// 景点Id
        /// </summary>
        public int DestId { get; set; }

        /// <summary>
        /// 门票类型Id
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 门票类型
        /// </summary>
        [ForeignKey("TypeId")]
        public TicketType TicketType { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        public double MarketPrice { get; set; }

        /// <summary>
        /// 商城价
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public double CostPrice { get; set; }

        /// <summary>
        /// 代理商价格
        /// </summary>
        public double AgentPrice { get; set; }

        /// <summary>
        /// 可以支付预付款
        /// </summary>
        public bool CanPayFrontMoney { get; set; }

        /// <summary>
        /// 预付款金额
        /// </summary>
        public bool FrontMoneyPrice { get; set; }

        /// <summary>
        /// 可以使用积分
        /// </summary>
        public bool CanUsePoint { get; set; }

        /// <summary>
        /// 使用积分数量
        /// </summary>
        public int UsePoints { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Inventory { get; set; }

        /// <summary>
        /// 门票描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 门票开始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 门票结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 报价类型
        /// </summary>
        public QuotesType QuotesType { get; set; }
    }

    public enum QuotesType
    {
        AllDays,
        Weekly,
        FixedDay
    }
}
