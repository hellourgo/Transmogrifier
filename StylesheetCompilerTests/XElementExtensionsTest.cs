using System.Linq;
using System.Xml.Linq;
using Transmogrifier.StylesheetCompiler.StylesheetGenerator;
using Transmogrifier.Xslt;
using Xunit;

namespace Transmogrifier.StylesheetCompilerTests
{
    public class XElementExtensionsTest
    {
        [Fact]
        public void AddAndReturnChild_Elements_AddChildToParent()
        {
            var parent = new XsltElement("Parent");
            var child = new XsltElement("Child");

            var returnedElement = parent.AddAndReturnChild(child);
            Assert.Same(child, returnedElement);
            Assert.Contains(parent.Elements(), e => e == child);
            Assert.Same(parent, child.Parent);
        }

        [Fact]
        public void FirstOrDefaultXsltElement_ElementName_ReturnsFirstElement()
        {
            var parent = new XsltElement("Parent");
            var childName = "testName";
            var child = new XsltElement("element", new XAttribute("name", childName));
            parent.Add(child);

            var foundElement = parent.FirstOrDefaultXsltElement(childName);

            Assert.Same(child, foundElement);
        }

        [Fact]
        public void FirstOrDefaultXsltElement_ElementName_ReturnsNull()
        {
            var parent = new XsltElement("Parent");
            var childName = "testName";
            var child = new XsltElement("element", new XAttribute("name", childName));
            parent.Add(child);

            var foundElement = parent.FirstOrDefaultXsltElement("someOtherName");

            Assert.Null(foundElement);
        }

        [Fact]
        public void FirstOrDefaultXsltElement_Element_ReturnsFirstElement()
        {
            var parent = new XsltElement("Parent");
            var childName = "testName";
            var child = new XsltElement("element", new XAttribute("name", childName));
            parent.Add(child);

            var foundElement = parent.FirstOrDefaultXsltElement(child);

            Assert.Same(child, foundElement);
        }

        [Fact]
        public void AddXsltElement_Element_AddsToEnd()
        {
            var firstChild = new XsltElement("FirstChild");
            var secondChild = new XsltElement("SecondChild");
            var parentElement = new XsltElement("Parent", firstChild, secondChild);
            var addedElement = new XsltElement("ElementContentType");

            parentElement.AddXsltElement(addedElement);

            Assert.Same(addedElement, parentElement.Elements().Last());
        }

        [Fact]
        public void AddXsltElement_Attribute_AddsFirst()
        {
            var firstChild = new XsltElement("FirstChild");
            var secondChild = new XsltElement("SecondChild");
            var parentElement = new XsltElement("Parent", firstChild, secondChild);
            var addedElement = new XsltElement(XsltElementType.Attribute, new XAttribute("name", "AttributeContentType"));

            parentElement.AddXsltElement(addedElement);

            Assert.Same(addedElement, parentElement.Elements().First());
        }
    }
}