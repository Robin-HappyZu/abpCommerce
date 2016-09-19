using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Trip
{
    public enum CountryType
    {
        /// <summary>
        /// 国内
        /// </summary>
        [Display(Name = "境内")]
        Domestic,
        /// <summary>
        /// 国外
        /// </summary>
        [Display(Name = "境外")]
        Abroad
    }
}
