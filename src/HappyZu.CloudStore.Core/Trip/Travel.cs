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
    /// 旅游线路
    /// </summary>
    [Table("Trip_Travel")]
    public class Travel:Entity,ISoftDelete,ISEO
    {
        /// <summary>
        /// 推广标题
        /// </summary>
        [StringLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        [StringLength(255)]
        public string Subject { get; set; }
        

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
        /// 出发地所属省份Id
        /// </summary>
        public int DepartureProvinceId { get; set; }

        /// <summary>
        /// 出发地所属省份、区域
        /// </summary>
        [ForeignKey("DepartureProvinceId")]
        public DestProvince DepartureProvince { get; set; }

        /// <summary>
        /// 出发地所属城市Id
        /// </summary>
        public int DepartureCityId { get; set; }
        /// <summary>
        /// 出发地所属城市
        /// </summary>
        [ForeignKey("DepartureCityId")]
        public DestCity DepartureCity { get; set; }
        
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
        /// 旅游天数
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 旅游夜晚数
        /// </summary>
        public int Nights { get; set; }

        /// <summary>
        /// 提前预定天数
        /// </summary>
        public int AdvancedBookingDays { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public TimeSpan EndTime { get; set; }


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
        /// 预定须知
        /// </summary>
        public string BookingNotice { get; set; }

        /// <summary>
        /// 游玩协议
        /// </summary>
        public string Agreement { get; set; }

        /// <summary>
        /// 行程简要
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 线路特点
        /// </summary>
        public string Feature { get; set; }

        /// <summary>
        /// 行程安排
        /// </summary>
        public string Schedule { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        [StringLength(255)]
        public string CoverImage { get; set; }

        /// <summary>
        /// 已发布
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishDateTime { get; set; }
        
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Seo标题
        /// </summary>
        [StringLength(255)]
        public string MetaTitle { get; set; }

        /// <summary>
        /// Seo关键词
        /// </summary>
        [StringLength(127)]
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Seo描述
        /// </summary>
        [StringLength(255)]
        public string MetaDescription { get; set; }
    }
}
