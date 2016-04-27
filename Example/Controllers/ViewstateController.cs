using Shared;
using System.IO;
using System.Text;
using System.Web;
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
            ViewData["value"] = "/wEPZA8FCjE1MDkyOTMyNjgPFgIeEFRleHRzSW5WaWV3c3RhdGUVBAZpdGVtIDEGaXRlbSAyBml0ZW0gMwZpdGVtIDQWAgIDD2QWAgIBDxYCHgtfIUl0ZW1Db3VudAIEFggCAQ9kFgJmDxUBBml0ZW0gMWQCAg9kFgJmDxUBBml0ZW0gMmQCAw9kFgJmDxUBBml0ZW0gM2QCBA9kFgJmDxUBBml0ZW0gNGQ=";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Decode(string value)
        {
            try
            {
                ViewData["rawLos"] = Newtonsoft.Json.JsonConvert.SerializeObject(new LosFormatter().Deserialize(value));
            }
            catch (System.Exception ex)
            {
                ViewData["rawLos"] = ex.Message;
            }
            try
            {
                var res = ViewStateXmlBuilder.BuildXml(new LosFormatter().Deserialize(value));

                ViewData["rawXml"] = res.DomString();

                ViewData["rawXmlControlState"] = res.ControlstateDomString();
            }
            catch (System.Exception ex)
            {
                ViewData["rawXml"] = ex.Message;
                ViewData["rawXmlControlState"] = ex.Message;
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