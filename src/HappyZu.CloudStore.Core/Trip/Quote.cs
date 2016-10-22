using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip
{
    [ComplexType]
    public class Quote
    {

        /// <summary>
        /// 市场价
        /// </summary>
        public decimal MarketPrice { get; set; }

        /// <summary>
        /// 商城价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 代理商价格
        /// </summary>
        public decimal AgentPrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Inventory { get; set; }
    }
}
