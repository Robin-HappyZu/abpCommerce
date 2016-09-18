using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMap(typeof(Dest))]
    public class DestDto : EntityDto
    {
        /// <summary>
        /// 推广标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 景点名称
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 景点特点
        /// </summary>
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
        /// 所属城市Id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 景点地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Lng { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Lat { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 供应商Id(备用)
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// 开放时间
        /// </summary>
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
        public string CoverImage { get; set; }

        public bool HasTicket { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        public bool IsDeleted { get; set; }

        public string MetaTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }
    }
}
