using Example.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers
{
    public class XSSController : Controller
    {
        public IActionResult Index()
        {
            ViewData["examplecode"] = @"<%= item.Data %> 
and in asp.net mvc: 
[ValidateInput(false)]
may be set on page or site level in classic asp.net using ValidateRequest";
            return View();
        }

        public IActionResult CrossDomain()
        {
            return View();
        }

        public IActionResult ShowEncoded()
        {
            ViewData.Model = Product.GetXSSUserProduct();
            return View();
        }

        public IActionResult ShowUnEncoded()
        {
            ViewData.Model = Product.GetXSSUserProduct();
            return View();
        }

        public IActionResult BBCode()
        {
            ViewData["value"] = "Something [b]bold[/b] and [i]italic[/i] or maybe [color=blue]blue[/color]. And unescaped html:<div>content of div</div>";
            return View();
        }

        [AcceptVerbs("POST")]
        public IActionResult BBCode(string value)
        {
            ViewData["value"] = value;

            return View();
        }


        public IActionResult Markdown()
        {
            ViewData["value"] = "";
            return View();
        }

        [AcceptVerbs("POST")]
        public IActionResult Markdown(string value)
        {
            ViewData["value"] = value;

            return View();
        }
        public IActionResult Adsafe()
        {
            return View();
        }
    }
}