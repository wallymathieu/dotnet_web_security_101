
using System;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Example.Controllers
{
    public class CrossSiteForgeryController : Controller
    {

        [AcceptVerbs("GET")]
        public IActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("GET")]
        public IActionResult UpdateWithAntiForgeryToken()
        {
            return View("Update");
        }
      

        [AcceptVerbs("POST")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateWithAntiForgeryToken(string text)
        {
            ViewData["message"] = string.Format("{0} submitted from UrlReferrer: {1}", text, Request.UrlReferrer());
            return View("Update");
        }

        [AcceptVerbs("POST")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateWithAntiForgeryTokenAjax(string text)
        {
            ViewData["message"] = string.Format("{0} submitted from {1}", text, Request.UrlReferrer());
            return Content((string)ViewData["message"]);
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        public IActionResult UpdateAjax(string text)
        {
            ViewData["message"] = string.Format("{0} submitted from {1}", text, Request.UrlReferrer());
            return Content((string)ViewData["message"]);
        }
    }
}