using System.Web.Mvc;

namespace Example.Controllers
{
    public class CrossSiteForgeryController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateWithAntiForgeryToken()
        {
            return View("Update");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateWithAntiForgeryToken(string text)
        {
            ViewData["message"] = string.Format("{0} submitted from {1}", text, Request.UrlReferrer);
            return View("Update");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateWithAntiForgeryTokenAjax(string text)
        {
            ViewData["message"] = string.Format("{0} submitted from {1}", text, Request.UrlReferrer);
            return Content((string)ViewData["message"]);
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateAjax(string text)
        {
            ViewData["message"] = string.Format("{0} submitted from {1}", text, Request.UrlReferrer);
            return Content((string)ViewData["message"]);
        }
    }
}