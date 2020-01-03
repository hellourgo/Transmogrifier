using System.Xml.Linq;
using Transmogrifier.Xslt;
using Xunit;

namespace Transmogrifier.XsltTests
{
    public class XsltElementTests
    {
        [Theory]
        [InlineData(XsltElementType.Key)]
        [InlineData(XsltElementType.Element)]
        [InlineData(XsltElementType.Import)]
        [InlineData(XsltElementType.Variable)]
        [InlineData(XsltElementType.Attribute)]
        public void Constructor_XsltElementType_TypeSetCorrectly(XsltElementType xsltElementType)
        {
            var xsltElement = new XsltElement(xsltElementType);
            Assert.Equal(xsltElementType, xsltElement.XsltElementType);
        }

        [Fact]
        public void Constructor_XsltElementNameXslNamespace_SetsXsltElementType()
        {
            var xslElement = new XsltElement(XsltElement.XslNamespace+"element");
            Assert.Equal(XsltElementType.Element, xslElement.XsltElementType);
        }

        [Fact]
        public void Constructor_NonXsltElementNameXslNamespace_SetsNoneXsltElementTypeAndNoneNamespace()
        {
            var xslElement = new XsltElement(XsltElement.XslNamespace + "whatever");
            Assert.Equal(XsltElementType.None, xslElement.XsltElementType);
            Assert.Equal(XNamespace.None, xslElement.Name.Namespace);
        }

        [Fact]
        public void Constructor_XsltElementNameXmlNamespace_SetsXsltElementTypeToDefault()
        {
            var xslElement = new XsltElement(XNamespace.Xml + "element");
            Assert.Equal(XsltElementType.None, xslElement.XsltElementType);
        }

        [Fact]
        public void Constructor_XsltElementNameNoNamespace_SetsXsltElementTypeAndXslNamespace()
        {
            var xslElement = new XsltElement("element");
            Assert.Equal(XsltElementType.Element, xslElement.XsltElementType);
            Assert.Equal(XsltElement.XslNamespace, xslElement.Name.Namespace);
        }

        [Fact]
        public void Constructor_NonXsltElementNameNoNamespace_SetsXsltElementTypeAndXslNamespaceToNone()
        {
            var xslElement = new XsltElement("whatever");
            Assert.Equal(XsltElementType.None, xslElement.XsltElementType);
            Assert.Equal(XNamespace.None, xslElement.Name.Namespace);
        }

        [Fact]
        public void Constructor_XsltElementType_SetsXsltElementTypeAndXslNamespace()
        {
            var type = XsltElementType.ApplyImports;
            var xslElement = new XsltElement(type);
            Assert.Equal(type, xslElement.XsltElementType);
            Assert.Equal(XsltElement.XslNamespace, xslElement.Name.Namespace);
        }

        [Theory]
        [InlineData("stylesheet", XsltElementType.Stylesheet)]
        [InlineData("attribute-set", XsltElementType.AttributeSet)]
        [InlineData("", XsltElementType.None)]
        [InlineData(null, XsltElementType.None)]
        public void ParseDescription_String_Returns(string description, XsltElementType expectedEnum)
        {
            var parsed = description.ParseXsltElementType();
            Assert.Equal(expectedEnum, parsed);
        }
    }
}