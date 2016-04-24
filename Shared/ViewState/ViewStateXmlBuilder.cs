using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;
using System.Xml;

namespace Shared
{
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

        public static XmlDocument BuildXml(object tree, out XmlDocument controlstateDom)
        {
            XmlDocument dom = new XmlDocument();
            controlstateDom = new XmlDocument();
            dom.AppendChild(dom.CreateElement("viewstate"));
            controlstateDom.AppendChild(controlstateDom.CreateElement("controlstate"));
            BuildElement(dom, dom.DocumentElement, tree, ref controlstateDom);
            return dom;
        }

        private static string GetShortTypename(object obj)
        {
            string str = obj.GetType().ToString();
            return str.Substring(str.LastIndexOf(".") + 1);
        }
    }
}
