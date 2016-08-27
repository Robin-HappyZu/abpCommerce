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
    [Table("FAQ_Detail")]
    public class FAQDetail:Entity,ISoftDelete
    {
        [StringLength(255)]
        public virtual string Title { get; set; }

        public virtual int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual FAQCategory Category { get; set; }

        public virtual string Discription { get; set; }

        public virtual int Sort { get; set; }
        
        public bool IsDeleted { get; set; }

        internal void Delete()
        {
            IsDeleted = true;
        }
    }
}
