using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happyzu.CloudStore.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 一天最开始时间，例：2014-04-05 17：30：20 ，结果 2014-04-05 00：00：00
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToStart(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
        }

        /// <summary>
        /// 本周最开始时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToWeekStart(this DateTime value)
        {
            int i = value.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。   
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return value.Subtract(ts).ToStart();
        }

        /// <summary>
        /// 本月最开始时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToMonthStart(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1, 0, 0, 0);
        }

        /// <summary>
        /// 本月最后时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToMonthEnd(this DateTime value)
        {
            return value.ToStart().AddMonths(1).AddSeconds(-1);
        }
    }
}
