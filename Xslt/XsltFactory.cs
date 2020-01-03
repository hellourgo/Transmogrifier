using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace Transmogrifier.Xslt
{
    /// <summary>
    /// Factory methods for creating select XSLT elements.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBeInternal")]
    public class XsltFactory
    {
        /// <summary>
        /// Creates an <c>xsl:apply-templates</c> element.
        /// <code>
        /// &lt;xsl:apply-templates select="<paramref name="select"/>" mode="<paramref name="mode"/>" /&gt;
        /// </code>
        /// </summary>
        /// <param name="select">The select.</param>
        /// <param name="mode">The mode.</param>
        public XsltElement ApplyTemplates(string select, string mode = null)
        {
            var element = new XsltElement(XsltElementType.ApplyTemplates, new XAttribute("select", select));
            element.SetAttributeValue("mode", mode);
            return element;
        }

        /// <summary>
        /// Creates an <c>xsl:attribute</c> element.
        /// <code>
        /// &lt;xsl:attribute name="<paramref name="name"/>" /&gt;
        /// </code>
        /// </summary>
        /// <param name="name">The name.</param>
        public XsltElement Attribute(string name) =>
            new XsltElement(XsltElementType.Attribute, new XAttribute("name", name));

        /// <summary>
        /// Creates an <c>xsl:copy</c> element.
        /// <code>
        /// &lt;xsl:copy /&gt;
        /// </code>
        /// </summary>
        public XsltElement Copy() => new XsltElement(XsltElementType.Copy);

        /// <summary>
        /// Creates an <c>xsl:element</c> element.
        /// <code>
        /// &lt;xsl:element name="<paramref name="name"/>" /&gt;
        /// </code>
        /// </summary>
        /// <param name="name">The name.</param>
        public XsltElement Element(string name) =>
            new XsltElement(XsltElementType.Element, new XAttribute("name", name));

        /// <summary>
        /// Creates an <c>xsl:key</c> element.
        /// <code>
        /// &lt;xsl:key name="<paramref name="name"/>" match="<paramref name="match"/>" use="<paramref name="use"/>" /&gt;
        /// </code>
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="match">The match.</param>
        /// <param name="use">The use.</param>
        public XsltElement Key(string name, string match, string use) => new XsltElement(XsltElementType.Key,
            new XAttribute("name", name), new XAttribute("match", match),
            new XAttribute("use", use));

        /// <summary>
        /// Creates an <c>xsl:number</c> element.
        /// <code>
        /// &lt;xsl:number /&gt;
        /// </code>
        /// </summary>
        public XsltElement Number() => new XsltElement(XsltElementType.Number);

        /// <summary>
        /// Creates an <c>xsl:output</c> element.
        /// <code>
        /// &lt;xsl:output method="<paramref name="outputMethod"/>" /&gt;
        /// </code>
        /// </summary>
        /// <param name="outputMethod">The output method.</param>
        /// <param name="indent">if set to <c>true</c> [indent].</param>
        public XsltElement Output(OutputMethod outputMethod = OutputMethod.Xml, bool indent = false)
        {
            var element = new XsltElement(XsltElementType.Output,
                new XAttribute("method", outputMethod.ToString().ToLower()));
            if (indent) element.SetAttributeValue("indent", "yes");
            return element;
        }

        /// <summary>
        /// Creates an <c>xsl:strip-space</c> element.
        /// <code>
        /// &lt;xsl:strip-space elements="<paramref name="elements"/>" /&gt;
        /// </code>
        /// </summary>
        /// <param name="elements">The elements.</param>
        public XsltElement StripSpace(string elements) =>
            new XsltElement(XsltElementType.StripSpace, new XAttribute("elements", elements));

        /// <summary>
        /// Creates an <c>xsl:template</c> element.
        /// <code>
        /// &lt;xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" /&gt;
        /// </code>
        /// </summary>
        public XsltElement Stylesheet() => new XsltElement(XsltElementType.Stylesheet,
            new XAttribute(XNamespace.Xmlns + "xsl", XsltElement.XslNamespace.NamespaceName),
            new XAttribute("version", "1.0"));

        /// <summary>
        /// Creates an <c>xsl:template</c> element.
        /// <code>
        /// &lt;xsl:value-of match="<paramref name="match"/>" mode="<paramref name="mode"/>" /&gt;
        /// </code>
        /// </summary>
        /// <param name="match">The match.</param>
        /// <param name="mode">The mode.</param>

        public XsltElement Template(string match, string mode = null)
        {
            var element = new XsltElement(XsltElementType.Template, new XAttribute("match", match));
            element.SetAttributeValue("mode", mode);
            return element;
        }

        /// <summary>
        /// Creates an <c>xsl:text</c> element.
        /// <code>
        /// &lt;xsl:value-of data="<paramref name="data"/>" /&gt;
        /// </code>
        /// </summary>
        /// <param name="data">The data.</param>
        public XsltElement Text(string data = null) => new XsltElement(XsltElementType.Text, data);

        /// <summary>
        /// Creates an <c>xsl:value-of</c> element.
        /// <code>
        /// &lt;xsl:value-of name="<paramref name="name"/>" /&gt;
        /// </code>
        /// </summary>
        /// <param name="name">The name.</param>
        public XsltElement ValueOf(string name) =>
            new XsltElement(XsltElementType.ValueOf, new XAttribute("select", name));

        /// <summary>
        /// Creates an <c>xsl:variable</c> element.
        /// <code>
        /// &lt;xsl:variable name="<paramref name="name"/>" /&gt;
        /// </code>
        /// </summary>
        /// <param name="name">The name.</param>
        public XsltElement Variable(string name) =>
            new XsltElement(XsltElementType.Variable, new XAttribute("name", name));
    }
}