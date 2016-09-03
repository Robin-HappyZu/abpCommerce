using System.ComponentModel.DataAnnotations;

namespace HappyZu.CloudStore.Trip
{
    /// <summary>
    /// 证件类型
    /// </summary>
    public enum IDType
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [Display(Name = "身份证")]
        IdentityCard,
        /// <summary>
        /// 护照
        /// </summary>
        [Display(Name = "护照")]
        Passport,
        /// <summary>
        /// 港澳通行证
        /// </summary>
        [Display(Name = "港澳通行证")]
        HongKongMacauPass,
        /// <summary>
        /// 台湾通行证
        /// </summary>
        [Display(Name = "台湾通行证")]
        TaiwanPass,
        /// <summary>
        /// 驾驶证
        /// </summary>
        [Display(Name = "驾驶证")]
        DrivingLicence,
        /// <summary>
        /// 台胞证
        /// </summary>
        [Display(Name = "台胞证")]
        MTP,
        /// <summary>
        /// 回乡证
        /// </summary>
        [Display(Name = "回乡证")]
        HomeVisitPermit,
        /// <summary>
        /// 军官证
        /// </summary>
        [Display(Name = "军官证")]
        CertificateOfOfficers,
        /// <summary>
        /// 外国人永久居留证
        /// </summary>
        [Display(Name = "外国人永久居留证")]
        AlienPermanentResidenceCertificate,
        /// <summary>
        /// 学生证
        /// </summary>
        [Display(Name = "学生证")]
        StudentCard,
        /// <summary>
        /// 国际海员证
        /// </summary>
        [Display(Name = "国际海员证")]
        InternationalSeamensCertificate,
        /// <summary>
        /// 警官证
        /// </summary>
        [Display(Name = "警官证")]
        OfficersCard,
        /// <summary>
        /// 其他
        /// </summary>
        [Display(Name = "其他")]
        Other
    }
}
