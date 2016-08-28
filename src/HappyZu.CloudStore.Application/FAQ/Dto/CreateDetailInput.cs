using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.FAQ.Dto
{
    public class CreateDetailInput
    {
        public CreateDetailInput()
        {
            IsDelete = false;
            Sort = 0;
        }

        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Discription { get; set; }
        public int Sort { get; set; }
        public bool IsDelete { get; set; }
    }
}
