using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Users.Dto
{
    public class SetPasswordInput
    {
        public string OldPassword { get; set; }

        [Required]
        public string Password { get; set; }
        
        public long UserId { get; set; }
    }
}
