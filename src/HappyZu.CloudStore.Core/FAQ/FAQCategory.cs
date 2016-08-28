using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace HappyZu.CloudStore.FAQ
{
    [Table("FAQ_Category")]
    public class FAQCategory:Entity,ISoftDelete
    {
        [StringLength(255)]
        public virtual string Name { get; set; }

        [StringLength(255)]
        public virtual string Icon { get; set; }

        [StringLength(50)]
        public virtual string FontIcon { get; set; }

        public virtual bool IsEnable { get; set; }

        public virtual int Sort { get; set; }

        public bool IsDeleted { get; set; }

        public IList<FAQDetail> Details { get; set; } 

        internal void Enable()
        {
            IsEnable = true;
        }

        internal void Disable()
        {
            IsEnable = false;
        }

        internal void Delete()
        {
            IsDeleted = true;
        }


        public static FAQCategory Create(string name,string icon,string fontIcon,int sort=0,bool isEnable=true,bool isDelete=false)
        {
            var @category = new FAQCategory()
            {
                Name = name,
                Icon = icon,
                FontIcon = fontIcon,
                IsEnable = isEnable,
                IsDeleted = isDelete,
                Sort = sort
            };
            @category.Details=new List<FAQDetail>();
            return @category;
        }
    }
}
