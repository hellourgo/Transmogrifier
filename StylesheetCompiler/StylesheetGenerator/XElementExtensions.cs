using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Transmogrifier.Xslt;

namespace Transmogrifier.StylesheetCompiler.StylesheetGenerator
{
    internal static class XElementExtensions
    {
        public static XsltElement AddAndReturnChild(this XsltElement parent, XsltElement child)
        {
            parent.Add(child);
            return child;
        }

        public static void AddOrUpdateOutputElements(this XsltElement root, XsltElement outputElement)
        {
            var existingElement = root.FirstOrDefaultXsltElement(outputElement);
            if (existingElement != null)
            {
                existingElement.Add(outputElement.XsltElements());
                return;
            }

            root.AddXsltElement(outputElement);
        }

        public static void AddXsltElement(this XElement element, XsltElement transformElement)
        {
            if (transformElement.XsltElementType == XsltElementType.Attribute)
                element.AddFirst(transformElement);
            else
                element.Add(transformElement);
        }

        public static XsltElement FirstOrDefaultXsltElement(this XsltElement root, string elementName)
        {
            return root?.XsltElements().FirstOrDefault(e => e.XsltElementType == XsltElementType.Element &&
                                                                            e.Attributes()
                                                                             .Any(a => a.Name == "name" && a.Value == elementName));
        }

        public static XsltElement FirstOrDefaultXsltElement(this XsltElement root, XsltElement element)
        {
            var elementName = element.Attribute("name")?.Value;
            return root.FirstOrDefaultXsltElement(elementName);
        }

        public static XsltElement GetPathEndpoint(this XsltElement rootElement, IEnumerable<XsltElement> pathPartElements) =>
            pathPartElements.Aggregate(rootElement, GetPathPartElement);

        public static XsltElement GetPathPartElement(XsltElement parentElement, XsltElement pathPartElement) =>
            parentElement.FirstOrDefaultXsltElement(pathPartElement) ??
            parentElement.AddAndReturnChild(pathPartElement);
    }
}