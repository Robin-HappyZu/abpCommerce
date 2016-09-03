using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Entities
{
    public interface ISEO
    {
        string MetaTitle { get; set; }

        string MetaKeywords { get; set; }

        string MetaDescription { get; set; }
    }
}
