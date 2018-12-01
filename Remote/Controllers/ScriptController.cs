using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Net.Http;
using System.Text;

namespace Remote.Controllers
{
    public class ScriptController : Controller
    {
        [HttpGet("/script/")]
        public IActionResult Get(string text, string callback)
        {
            var res = $"{text} submitted from {Request.UrlReferrer()} with callback: {callback}, origin: {Request.Origin()}";
            return Content(callback + "({message:'" + res + "'});", "application/javascript", Encoding.UTF8);
        }

        [HttpPost("/script/")]
        [EnableCors("Localhost")]
        public IActionResult Post([FromBody]string text)
        {
            var res = $"{text} submitted from {Request.UrlReferrer()}, origin: {Request.Origin()}";
            return Json(new { message =res});
        }
    }
}
