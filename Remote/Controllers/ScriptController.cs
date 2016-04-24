using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Remote.Controllers
{
    public class ScriptController : ApiController
    {
        public HttpResponseMessage Get(string text, string callback)
        {
            var res = string.Format("{0} submitted from {1} with callback: {2}", text, HttpContext.Current.Request.UrlReferrer, callback);
            return new HttpResponseMessage()
            {
                Content = new StringContent(callback + "({message:'" + res + "'});",
                              Encoding.UTF8,
                              "application/javascript")
            };
        }
    }
}
