using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class DataTableJsonViewModel
    {
        /// <summary>
        /// 渲染次数
        /// </summary>
        public int draw { get; set; }
        /// <summary>
        /// 记录总条数
        /// </summary>
        public int recordsTotal { get; set; }

        /// <summary>
        /// 过滤后条数
        /// </summary>
        public int recordsFiltered { get; set; }

        /// <summary>
        /// 自定义执行消息
        /// </summary>
        public string customActionMessage { get; set; }

        /// <summary>
        /// 自定义执行状态
        /// </summary>
        public string customActionStatus { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object data { get; set; }

        public DataTableJsonViewModel() { }


        public DataTableJsonViewModel(int draw, int total, object result)
            : this(draw, total, total, true, null, result)
        {
        }

        public DataTableJsonViewModel(int draw, int total, int filterCount, bool success, string msg, object result)
        {
            this.draw = draw;
            this.recordsTotal = total;
            this.recordsFiltered = filterCount;
            this.customActionMessage = msg;
            this.customActionStatus = success ? "OK" : string.Empty;
            this.data = result;
        }
    }
}