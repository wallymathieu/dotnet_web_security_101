using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Xml;

namespace Shared.ViewState
{
    public class ViewStateInXml
    {
        public readonly XmlDocument ControlstateDom;
        public readonly XmlDocument Dom;

        private string ToString(XmlDocument doc)
        {
            var sb = new StringBuilder();
            doc.Save(new StringWriter(sb));
            return sb.ToString();
        }

        public string DomString() { return ToString(Dom); }
        public string ControlstateDomString() { return ToString(ControlstateDom); }

        public ViewStateInXml(XmlDocument dom, XmlDocument controlstateDom)
        {
            this.Dom = dom;
            this.ControlstateDom = controlstateDom;
        }
    }

    /// <summary>
    /// look at http://aspalliance.com/articleViewer.aspx?aId=135&pId=
    /// </summary>
    public class ViewStateXmlBuilder
    {
        // Methods
        private static void BuildElement(XmlDocument dom, XmlElement elem, object treeNode, ref XmlDocument controlstateDom)
        {
            if (treeNode != null)
            {
                XmlElement element;
                Type type = treeNode.GetType();
                if (type == typeof(Triplet))
                {
                    element = dom.CreateElement(GetShortTypename(treeNode));
                    elem.AppendChild(element);
                    BuildElement(dom, element, ((Triplet)treeNode).First, ref controlstateDom);
                    BuildElement(dom, element, ((Triplet)treeNode).Second, ref controlstateDom);
                    BuildElement(dom, element, ((Triplet)treeNode).Third, ref controlstateDom);
                }
                else if (type == typeof(Pair))
                {
                    element = dom.CreateElement(GetShortTypename(treeNode));
                    elem.AppendChild(element);
                    BuildElement(dom, element, ((Pair)treeNode).First, ref controlstateDom);
                    BuildElement(dom, element, ((Pair)treeNode).Second, ref controlstateDom);
                }
                else if (type == typeof(ArrayList))
                {
                    element = dom.CreateElement(GetShortTypename(treeNode));
                    elem.AppendChild(element);
                    foreach (object treeNode1 in (ArrayList)treeNode)
                    {
                        BuildElement(dom, element, treeNode1, ref controlstateDom);
                    }
                }
                else if (treeNode is Array)
                {
                    element = dom.CreateElement("Array");
                    elem.AppendChild(element);
                    foreach (object treeNode1 in (Array)treeNode)
                    {
                        BuildElement(dom, element, treeNode1, ref controlstateDom);
                    }
                }
                else if (treeNode is HybridDictionary)
                {
                    element = controlstateDom.CreateElement(GetShortTypename(treeNode));
                    controlstateDom.DocumentElement.AppendChild(element);
                    foreach (object treeNode1 in (HybridDictionary)treeNode)
                    {
                        BuildElement(controlstateDom, element, treeNode1, ref controlstateDom);
                    }
                }
                else if (treeNode is DictionaryEntry)
                {
                    element = dom.CreateElement(GetShortTypename(treeNode));
                    elem.AppendChild(element);
                    DictionaryEntry entry = (DictionaryEntry)treeNode;
                    BuildElement(dom, element, entry.Key, ref controlstateDom);
                    DictionaryEntry entry2 = (DictionaryEntry)treeNode;
                    BuildElement(dom, element, entry2.Value, ref controlstateDom);
                }
                else
                {
                    element = dom.CreateElement(GetShortTypename(treeNode));
                    if (type == typeof(IndexedString))
                    {
                        element.InnerText = ((IndexedString)treeNode).Value;
                    }
                    else
                    {
                        element.InnerText = treeNode.ToString();
                    }
                    elem.AppendChild(element);
                }
            }
        }

        public static ViewStateInXml BuildXml(object tree)
        {
            var dom = new XmlDocument();
            var controlstateDom = new XmlDocument();
            dom.AppendChild(dom.CreateElement("viewstate"));
            controlstateDom.AppendChild(controlstateDom.CreateElement("controlstate"));
            BuildElement(dom, dom.DocumentElement, tree, ref controlstateDom);
            return new ViewStateInXml(dom, controlstateDom);
        }

        private static string GetShortTypename(object obj)
        {
            string str = obj.GetType().ToString();
            return str.Substring(str.LastIndexOf(".") + 1);
        }
    }
}
