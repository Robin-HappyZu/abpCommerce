using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip
{
    /// <summary>
    /// 取票人
    /// </summary>
    [Table("Trip_TicketCollectingPerson")]
    public class TicketCollectingPerson
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [StringLength(255)]
        public string Name { get; set; }
        
        /// <summary>
        /// 电话号码归属
        /// </summary>
        public DestType PhoneDestType { get; set; }

        /// <summary>
        /// 国家代码
        /// </summary>
        [StringLength(15)]
        public string ContryNumber { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(127)]
        public string Mobile { get; set; }

        /// <summary>
        /// Email
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
        public double Insurance { get; set; }

        /// <summary>
        /// 是取票人
        /// </summary>
        public bool IsCollectingTicketsPerson { get; set; }
    }
}
