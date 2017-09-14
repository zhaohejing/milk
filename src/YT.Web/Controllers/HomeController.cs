using System.Web.Mvc;

namespace YT.Web.Controllers
{
    public class HomeController : AbpZeroTemplateControllerBase
    {
        public JsonResult Index()
        {
            Response.Redirect("/swagger");

            return Json(new {});
        }
    }
}