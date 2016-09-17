using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Controllers
{
    public class MobileControllerBase : AbpController
    {
        protected MobileControllerBase()
        {
            LocalizationSourceName = CloudStoreConsts.LocalizationSourceName;
            //隐藏底部
            ViewBag.HideFootBar = true;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}