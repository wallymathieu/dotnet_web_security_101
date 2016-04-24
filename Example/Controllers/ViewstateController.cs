using Shared;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;

namespace Example.Controllers
{
    public class ViewstateController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Decode()
        {
            ViewData["value"] = "";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Decode(string value)
        {
            XmlDocument controlstateDom;
            XmlDocument dom = ViewStateXmlBuilder.BuildXml(new LosFormatter().Deserialize(value), out controlstateDom);

            {
                var sb = new StringBuilder();
                dom.Save(new StringWriter(sb));
                ViewData["rawXml"] = sb.ToString();
            }
            //
            {
                var sb = new StringBuilder();
                controlstateDom.Save(new StringWriter(sb));
                ViewData["rawXmlControlState"] = sb.ToString();
            }
            //Raw Base64
            {
                string data = ViewStateHelper.GetRawBase64Data(value);
                ViewData["rawBase64"] = data;
            }
            return View();
        }


    }
}