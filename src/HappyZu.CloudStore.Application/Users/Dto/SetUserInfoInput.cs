using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Users.Dto
{
    public class SetUserInfoInput
    {
        public long UserId { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Name { get; set; }

        public string Mobile { get; set; }

        public string EmailAddress { get; set; }


    }
}
