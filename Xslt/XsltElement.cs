using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace Transmogrifier.Xslt
{
    /// <inheritdoc />
    [SuppressMessage("ReSharper", "MemberCanBeInternal")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class XsltElement : XElement
    {
        /// <summary>
        ///     The W3C XSLT Namespace
        /// </summary>
        public static readonly XNamespace XslNamespace = XNamespace.Get("http://www.w3.org/1999/XSL/Transform");

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Transmogrifier.Xslt.XsltElement" /> class with the specified
        ///     <see cref="T:Transmogrifier.Xslt.XsltElementType" />.
        /// </summary>
        /// <param name="xsltElementType">Type of the XSLT element.</param>
        public XsltElement(XsltElementType xsltElementType) : base(XslNamespace + xsltElementType.GetDescription()) =>
            XsltElementType = GetXsltElementType(XslNamespace + xsltElementType.GetDescription());

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Transmogrifier.Xslt.XsltElement" /> class with the specified
        ///     <see cref="T:Transmogrifier.Xslt.XsltElementType" /> and content.
        /// </summary>
        /// <param name="xsltElementType">Type of the XSLT element.</param>
        /// <param name="content">The content.</param>
        public XsltElement(XsltElementType xsltElementType, object content) :
            base(XslNamespace + xsltElementType.GetDescription(), content) => XsltElementType =
            GetXsltElementType(XslNamespace + xsltElementType.GetDescription());

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Transmogrifier.Xslt.XsltElement" /> class with the specified
        ///     <see cref="T:Transmogrifier.Xslt.XsltElementType" /> and content.
        /// </summary>
        /// <param name="xsltElementType">Type of the XSLT element.</param>
        /// <param name="content">The content.</param>
        public XsltElement(XsltElementType xsltElementType, params object[] content) :
            base(XslNamespace + xsltElementType.GetDescription(), content) => XsltElementType =
            GetXsltElementType(XslNamespace + xsltElementType.GetDescription());

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Transmogrifier.Xslt.XsltElement" /> class with the specified
        ///     name.
        /// </summary>
        /// <param name="name">
        ///     An <see cref="T:System.Xml.Linq.XName" /> that contains the element name. If the XName does not coincide with
        ///     one from the XSLT 1.0 specification, the element will be a normal XML element.
        /// </param>
        public XsltElement(XName name) : base(name) => XsltElementType = GetXsltElementType(name);

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Transmogrifier.Xslt.XsltElement" /> class with the specified
        ///     name and content.
        /// </summary>
        /// <param name="name">
        ///     An <see cref="T:System.Xml.Linq.XName" /> that contains the element name. If the XName does not coincide with
        ///     one from the XSLT 1.0 specification, the element will be a normal XML element.
        /// </param>
        /// <param name="content">The contents of the element.</param>
        public XsltElement(XName name, object content) : base(name, content) =>
            XsltElementType = GetXsltElementType(name);

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Transmogrifier.Xslt.XsltElement" /> class with the specified
        ///     name and content.
        /// </summary>
        /// <param name="name">
        ///     An <see cref="T:System.Xml.Linq.XName" /> that contains the element name. If the XName does not coincide with
        ///     one from the XSLT 1.0 specification, the element will be a normal XML element.
        /// </param>
        /// <param name="content">The initial content of the element.</param>
        public XsltElement(XName name, params object[] content) : base(name, content) =>
            XsltElementType = GetXsltElementType(name);

        /// <summary>
        ///     Gets the type of the XSLT element.
        /// </summary>
        /// <value>
        ///     The type of the XSLT element.
        /// </value>
        public XsltElementType XsltElementType { get; }

        /// <summary>
        /// Returns child elements that are also <see cref="T:Transmogrifier.Xslt.XsltElement" />.
        /// </summary>
        public IEnumerable<XsltElement> XsltElements() => Elements().Cast<XsltElement>();

        private XsltElementType GetXsltElementType(XName name)
        {
            var xsltElementType = name.LocalName.ParseXsltElementType();

            SetName(xsltElementType);

            return Name.Namespace == XslNamespace && xsltElementType != XsltElementType.None
                ? xsltElementType
                : XsltElementType.None;
        }

        private void SetName(XsltElementType xsltElementType)
        {
            if (Name.Namespace == XNamespace.None && xsltElementType != XsltElementType.None)
                Name = XslNamespace + Name.LocalName;

            if (Name.Namespace == XslNamespace && xsltElementType == XsltElementType.None)
                Name = XNamespace.None + Name.LocalName;
        }
    }
}