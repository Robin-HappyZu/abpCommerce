using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HappyZu.CloudStore.FileManager;
using HappyZu.CloudStore.FileManager.Dto;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    public class UploadFileController : AdminControllerBase
    {
        private readonly IUploadFileService _fileItemManager;
        public UploadFileController(IUploadFileService fileItemManager)
        {
            _fileItemManager = fileItemManager;
        }

        // GET: Admin/Upload
        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost,ActionName("Index")]
        public async Task<ActionResult> IndexPost()
        {
            var files = Request.Files;
            if (files.Count > 0)
            {
                var file = files[0];

                var dir = Request["dir"];
                string filepath = string.Concat("~/Media/", dir);
                if (!filepath.EndsWith("/"))
                {
                    filepath += "/";
                }
                filepath += DateTime.Now.ToString("yyyyMM");
                var newfilename = DateTime.Now.ToString("ddHHmmssfffffff") + file.FileName.Substring(file.FileName.LastIndexOf("."));

                var path = Server.MapPath(filepath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var strFileName = string.Concat(filepath.Substring(1), "/", newfilename);

                if (!string.IsNullOrEmpty(file.FileName))
                {
                    try
                    {
                        var fileName = Path.Combine(path, newfilename);
                        file.SaveAs(fileName);

                        var result = await _fileItemManager.AddFileItem(new FileItemInput());

                        return Json(new { error = false, message = strFileName,id= result.EntityId });
                    }
                    catch (Exception ex)
                    {
                        return Json(new { error = true, message = ex.Message });
                    }
                   
                }
            }
            return Json(new { error = true, message = "请选择文件！" });
        }
    }
}