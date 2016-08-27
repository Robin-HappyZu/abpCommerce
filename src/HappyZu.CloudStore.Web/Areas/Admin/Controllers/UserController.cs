using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {

        #region 用户资料
        /// <summary>
        /// 用户概要
        /// Robin Z.
        /// 2016-05-25
        /// </summary>
        /// <returns></returns>
        public new ActionResult Profile()
        {
            return View();
        }

        /// <summary>
        /// 用户资料设置
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountSetting()
        {
            return View();
        }
        #endregion

        #region 用户银行卡

        public ActionResult UserBanks()
        {
            return View();
        }
        #endregion

        #region 用户收货地址
        /// <summary>
        /// 用户收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Address()
        {
            return View();
        }
        #endregion

        #region 用户上下级关系

        #endregion

        #region 用户等级

        #endregion

        #region 用户列表
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
        #endregion

        #region 用户等级
        /// <summary>
        /// 用户等级
        /// </summary>
        /// <returns></returns>
        public ActionResult Grades()
        {
            return View();
        }
        #endregion

        #region 用户积分
        /// <summary>
        /// 用户积分
        /// </summary>
        /// <returns></returns>
        public ActionResult Points()
        {
            return View();
        }
        #endregion

        #region 用户营销推广
        /// <summary>
        /// 用户营销推广
        /// </summary>
        /// <returns></returns>
        public ActionResult Spreads()
        {
            return View();
        }
        #endregion

        #region 用户关系
        /// <summary>
        /// 用户关系
        /// </summary>
        /// <returns></returns>
        public ActionResult Relation()
        {
            return View();
        }
        #endregion

        #region 升级日志
        /// <summary>
        /// 升级日志
        /// </summary>
        /// <returns></returns>
        public ActionResult UpgradeLogs()
        {
            return View();
        }
        #endregion
    }
}