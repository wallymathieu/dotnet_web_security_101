using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

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

        [EnableCors(origins: "http://localhost:8090", headers: "*", methods: "*")]
        public HttpResponseMessage Post([FromBody]string text)
        {
            var res = string.Format("{0} submitted from {1}", text, HttpContext.Current.Request.UrlReferrer);
            return new HttpResponseMessage()
            {
                Content = new StringContent("{\"message\":\"" + res + "\"}",
                              Encoding.UTF8,
                              "application/json")
            };
        }
    }
}
