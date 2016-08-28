using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.FAQ.Dto
{
    public class CreateFAQCategoryInput
    {
        public CreateFAQCategoryInput()
        {
            IsDelete = false;
            IsEnable = true;
            Sort = 0;
        }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string FontIcon { get; set; }
        public int Sort { get; set; }
        public bool IsEnable { get; set; }
        public bool IsDelete { get; set; }
    }
}
