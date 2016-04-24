using Example.Models;
using System.Web.Mvc;

namespace Example.Controllers
{
    public class XSSController : Controller
    {
        public ActionResult Index()
        {
            ViewData["examplecode"] = @"<%= item.Data %> 
and in asp.net mvc: 
[ValidateInput(false)]
may be set on page or site level in classic asp.net using ValidateRequest";
            return View();
        }

        public ActionResult CrossDomain()
        {
            return View();
        }

        public ActionResult ShowEncoded()
        {
            ViewData.Model = Product.GetXSSUserProduct();
            return View();
        }

        public ActionResult ShowUnEncoded()
        {
            ViewData.Model = Product.GetXSSUserProduct();
            return View();
        }

        public ActionResult BBCode()
        {
            ViewData["value"] = "Something [b]bold[/b] and [i]italic[/i] or maybe [color=blue]blue[/color]. And unescaped html:<div>content of div</div>";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BBCode(UnEncodedPotentialHtml value)
        {
            ViewData["value"] = value.Value;

            return View();
        }


        public ActionResult Markdown()
        {
            ViewData["value"] = "";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Markdown(UnEncodedPotentialHtml value)
        {
            ViewData["value"] = value.Value;

            return View();
        }
        public ActionResult Adsafe()
        {
            return View();
        }
    }
}