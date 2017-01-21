using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Net.Http;
using System.Text;

namespace Remote.Controllers
{
    public class ScriptController : Controller
    {
        public HttpResponseMessage Get(string text, string callback)
        {
            var res = string.Format("{0} submitted from {1} with callback: {2}", text, Request.UrlReferrer(), callback);
            return new HttpResponseMessage()
            {
                Content = new StringContent(callback + "({message:'" + res + "'});",
                              Encoding.UTF8,
                              "application/javascript")
            };
        }

        [EnableCors("Localhost")]
        public HttpResponseMessage Post([FromBody]string text)
        {
            var res = string.Format("{0} submitted from {1}", text, Request.UrlReferrer());
            return new HttpResponseMessage()
            {
                Content = new StringContent("{\"message\":\"" + res + "\"}",
                              Encoding.UTF8,
                              "application/json")
            };
        }
    }
}
