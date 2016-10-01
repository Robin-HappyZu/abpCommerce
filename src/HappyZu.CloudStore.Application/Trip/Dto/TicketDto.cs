﻿using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Trip.Dto
{
    [AutoMap(typeof(Ticket))]
    public class TicketDto : EntityDto
    {
        /// <summary>
        /// 景点Id
        /// </summary>
        public int DestId { get; set; }

        /// <summary>
        /// 门票名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 门票类型Id
        /// </summary>
        public int TypeId { get; set; }

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
        /// 必须提前
        /// </summary>
        public bool MustAdvance { get; set; }

        /// <summary>
        /// 提前预定天数
        /// </summary>
        public int AdvanceBookingDays { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// 报价类型
        /// </summary>
        public QuotesType QuotesType { get; set; }

        public bool IsDeleted { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
    }
}
