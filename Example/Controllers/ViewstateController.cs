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
            ViewData["value"] = "yRHLG6ndakgLf6bHf3faRizTy7GxdlWaP1g7Ih6nikUizj4j+OU1ww3S/TjZfeN26UqSuVmrbJgRaUN88/izFR0ZaBfoqjmQ0G7ncso+lQk8sNs8uiEeCBcM1F+UEdlxvJ6unK2cCaQE6nfPi+fR3OLna7jMmupHEaDgmzhKkscaDzDkD1IM+gREVfCuD1Xrf+FT5SKzSfHqdOvPI+R2eWPDuBi/uiT2SwN6uJSudTLoYcFcmZnbGt6bHyVdUODCtxNDOcdkTtwn9iXL417k3pP7EmORwGXhaeFQu1WQKcj1VymANdj32ePxkPksboBhrfd9M/yZowpq27M+iE9WKQ==";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Decode(string value)
        {
            //XmlDocument controlstateDom;
            //XmlDocument dom = ViewStateXmlBuilder.BuildXml(new LosFormatter().Deserialize(value), out controlstateDom);
            //var los = new LosFormatter().Deserialize(value);
            //ViewData["rawLos"] = los;
            //{
            //    var sb = new StringBuilder();
            //    //dom.Save(new StringWriter(sb));
            //    ViewData["rawXml"] = sb.ToString();
            //}
            ////
            //{
            //    var sb = new StringBuilder();
            //  //  controlstateDom.Save(new StringWriter(sb));
            //    ViewData["rawXmlControlState"] = sb.ToString();
            //}
            //Raw Base64
            {
                string data = ViewStateHelper.GetRawBase64Data(value);
                ViewData["rawBase64"] = data;
            }
            return View();
        }


    }
}