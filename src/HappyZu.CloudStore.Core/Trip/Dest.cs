using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using HappyZu.CloudStore.Entities;

namespace HappyZu.CloudStore.Trip
{
    /// <summary>
    /// 景点
    /// </summary>
    [Table("Trip_Dest")]
    public class Dest:Entity,ISoftDelete,ISEO
    {
        /// <summary>
        /// 推广标题
        /// </summary>
        [StringLength(255)]
        public string Title { get;set; }

        /// <summary>
        /// 景点名称
        /// </summary>
        [StringLength(255)]
        public string Subject { get; set; }

        /// <summary>
        /// 景点特点
        /// </summary>
        [StringLength(255)]
        public string Feature { get; set; }

        /// <summary>
        /// 国内国外
        /// </summary>
        public CountryType DestType { get; set; }

        /// <summary>
        /// 所属省份Id
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 所属省份、区域
        /// </summary>
        [ForeignKey("ProvinceId")]
        public DestProvince Province { get; set; }

        /// <summary>
        /// 所属城市Id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 所属城市
        /// </summary>
        [ForeignKey("CityId")]
        public DestCity City { get; set; }

        /// <summary>
        /// 景点地址
        /// </summary>
        [StringLength(255)]
        public string Address { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [StringLength(127)]
        public string Lng { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [StringLength(127)]
        public string Lat { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        [StringLength(255)]
        public string Supplier { get; set; }

        /// <summary>
        /// 供应商Id(备用)
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// 开放时间
        /// </summary>
        [StringLength(255)]
        public string OpenTime { get; set; }
        
        /// <summary>
        /// 最低价格
        /// </summary>
        public double MinPrice { get; set; }

        /// <summary>
        /// 最高价格
        /// </summary>
        public double MaxPrice { get; set; }
        
        /// <summary>
        /// 预定须知
        /// </summary>
        public string BookingNotice { get; set; }

        /// <summary>
        /// 游玩协议
        /// </summary>
        public string Agreement { get; set; }

        /// <summary>
        /// 景点介绍
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        [StringLength(255)]
        public string CoverImage { get; set; }

        public bool IsPublished { get; set; }

        public DateTime PublishDateTime { get; set; }

        public bool HasTicket { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        public bool IsDeleted { get; set; }

        [StringLength(255)]
        public string MetaTitle { get; set; }

        [StringLength(127)]
        public string MetaKeywords { get; set; }

        [StringLength(255)]
        public string MetaDescription { get; set; }
    }
    
}
