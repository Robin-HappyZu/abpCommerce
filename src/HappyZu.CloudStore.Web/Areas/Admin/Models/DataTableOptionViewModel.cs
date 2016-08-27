using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyZu.CloudStore.Web.Areas.Admin.Models
{
    public class DataTableOptionViewModel
    {
        public IList<Dictionary<string, string>> columns { get; set; }

        public int draw { get; set; }

        public int length { get; set; }

        public IList<Dictionary<string, string>> order { get; set; }

        public Dictionary<string, string> search { get; set; }

        public int start { get; set; }

        public string customActionType { get; set; }

        public string customActionName { get; set; }

        public Dictionary<string, string[]> id { get; set; }

        public Dictionary<string, string> filter { get; set; }

    }

    public class LongFromTo
    {
        public long From { get; set; }
        public long To { get; set; }
    }
    public class IntFromTo
    {
        public int From { get; set; }
        public int To { get; set; }
    }

    public class TimeFromTo
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}