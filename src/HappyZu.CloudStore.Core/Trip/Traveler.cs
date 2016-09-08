using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.Trip
{
    /// <summary>
    /// 出行人
    /// </summary>
    [Table("Trip_Traveler")]
    public class Traveler:Entity
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(127)]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public IDType IDType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [StringLength(255)]
        public string IDNumber { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }


        /// <summary>
        /// 是被保险人
        /// </summary>
        public bool IsInsured { get; set; }

        /// <summary>
        /// 保险费
        /// </summary>
        public double InsurancePremium { get; set; }

        /// <summary>
        /// 保险Id
        /// </summary>
        public int InsuranceId { get; set; }
    }
}
