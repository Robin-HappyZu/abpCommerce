using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.Agents
{
    [Table("Agents_Rebate")]
    public class Rebate : Entity
    {
        [StringLength(50)]
        public string OrderType { get; set; }

        [StringLength(63)]
        public string OrderNo { get; set; }

        public string UserName { get; set; }

        public DateTime PaidTime { get; set; }

        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 预计收入
        /// </summary>
        public decimal RebateAmount { get; set; }

        /// <summary>
        /// 真实收入
        /// </summary>
        public decimal IncomeAmount { get; set; }

        public DateTime ExpectedRebateDate { get; set; }
        
        public DateTime RebateDate { get; set; }
    }
}
