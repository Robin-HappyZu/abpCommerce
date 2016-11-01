using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace HappyZu.CloudStore.Users.Dto
{
    [AutoMap(typeof(User))]
    public class CreateUserInput 
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }


        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }


        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(User.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public string UnionID { get; set; }

        public string WechatOpenID { get; set; }

        public bool IsActive { get; set; }

        public bool IsSubscribe { get; set; }
    }
}