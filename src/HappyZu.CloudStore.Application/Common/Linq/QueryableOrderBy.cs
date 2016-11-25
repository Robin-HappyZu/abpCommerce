using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Common.Linq
{
    public class QueryableOrderBy
    {
        public string OrderColumn { get; set; }

        public OrderType OrderType { get; set; } 
    }

    public enum OrderType
    {
        OrderBy,
        OrderByDescending,
        ThenBy,
        ThenByDescending
    }
}
