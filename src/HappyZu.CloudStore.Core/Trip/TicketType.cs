using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.Trip
{
    public class TicketType:ISoftDelete
    {
        /// <summary>
        /// 景点Id
        /// </summary>
        public int DestId { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
