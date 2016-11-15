using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Wechat.Dto
{
    public class WechatPayResult
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 支付服务商订单编号
        /// </summary>
        public string TransactionNo { get; set; }
        /// <summary>
        /// 公众号OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 是否关注公众号
        /// </summary>
        public bool IsSubscribe { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        public decimal CashAmount { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        public DateTime PaidTime { get; set; }
    }
}
