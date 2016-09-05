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
        /// 库存
        /// </summary>
        public int Inventory { get; set; }
    }
}
