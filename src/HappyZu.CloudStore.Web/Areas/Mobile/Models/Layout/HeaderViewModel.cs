using System.Collections.Generic;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout
{
    public class HeaderViewModel
    {
        public HeaderViewModel()
        {
            LeftButtonItems=new List<BarButtonItem>();
            RightButtonItems=new List<BarButtonItem>();
        }

        public bool ShowTitle { get; set; }

        public string Title { get; set; }

        public IList<BarButtonItem> LeftButtonItems;

        public IList<BarButtonItem> RightButtonItems;

        public bool ShowSearchBar { get; set; }
    }

    public class BarButtonItem
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public string DisplayName { get; set; }

        public string Url { get; set; }
        
        public int Order { get; set; }
    }
    
}