using System.Linq;
using System.Xml.Linq;
using System.Xml.Xsl;
using Transmogrifier.Chrysalis;
using Transmogrifier.StylesheetCompiler.TransformBuilder;
using Xunit;

namespace Transmogrifier.StylesheetCompilerTests
{
    public class TransformBuilderTests
    {
        [Fact]
        public void Append_MultipleValues_AddsObjectsToRoot()
        {
            var builder = new TransformBuilder();
            var testElement = new XElement("TestElement");
            var testAttribute = new XAttribute("TestAttribute", "TestAttributeValue");
            builder.Append(testElement, testAttribute);

            var document = builder.Build();
            var rootNode = document.Root;
            Assert.NotNull(rootNode);
            Assert.Contains(rootNode.Elements(), e => e.Name.LocalName == "TestElement");
            Assert.Contains(rootNode.Attributes(),
                e => e.Name.LocalName == "TestAttribute" && e.Value == "TestAttributeValue");
        }

        [Fact]
        public void Append_NullValue_IsIgnored()
        {
            var builder = new TransformBuilder();

            builder.Append(null);

            var document = builder.Build();

            Assert.NotNull(document.Root);
        }

        [Fact]
        public void Append_ReturnedBuilderIsSameObject()
        {
            var builder = new TransformBuilder();
            var testElement = new XElement("TestElement");

            var returnedBuilder = builder.Append(testElement);

            Assert.Same(builder, returnedBuilder);
        }

        [Fact]
        public void Append_SingleValue_AddsObjectToRoot()
        {
            var builder = new TransformBuilder();
            var testElement = new XElement("TestElement");
            builder.Append(testElement);

            var document = builder.Build();
            var rootNode = document.Root;
            Assert.NotNull(rootNode);
            Assert.Contains(rootNode.Elements(), e => e == testElement);
        }

        [Fact]
        public void Build_ConfigIgnoreWhitespaceIsFalse_RemovesStripSpaceCorrectly()
        {
            var config = new TransformBuilderConfiguration
            {
                IgnoreWhitespace = false
            };
            var builder = new TransformBuilder(config);
            var document = builder.Build();
            builder.Initialize();
            var rootNode = document.Root;

            Assert.NotNull(rootNode);
            Assert.DoesNotContain(rootNode.Elements(), e => e.Name.LocalName == "strip-space");
        }

        [Fact]
        public void Build_DefaultConfiguration_DocumentHasCorrectRootNode()
        {
            var builder = new TransformBuilder();
            var document = builder.Build();

            Assert.NotNull(document.Root);
            Assert.Equal("stylesheet", document.Root.Name.LocalName);
        }

        [Fact]
        public void Build_DefaultConfiguration_DocumentIsValidXslt()
        {
            var builder = new TransformBuilder();
            var document = builder.Build();
            var transform = new XslCompiledTransform();
            using (var reader = document.CreateReader())
            {
                transform.Load(reader);
            }
        }

        [Fact]
        public void Initialize_DefaultConfiguration_RootHasCorrectSubNodes()
        {
            var builder = new TransformBuilder();
            builder.Initialize();
            var document = builder.Build();
            var rootNode = document.Root;

            Assert.NotNull(rootNode);
            Assert.Equal(3, rootNode.Elements().Count());
            Assert.Contains(rootNode.Elements(), e => e.Name.LocalName == "strip-space");
            Assert.Contains(rootNode.Elements(), e => e.Name.LocalName == "output");
            Assert.Contains(rootNode.Elements(), e => e.Name.LocalName == "template");
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CreateOutputElement_CreatesIndentAttributeCorrectly(bool formatXml)
        {
            var builder = new TransformBuilder();
            var element = builder.CreateOutput(OutputType.Xml, formatXml);

            Assert.Equal(formatXml, element.Attributes().Any(a => a.Name == "indent"));
        }

        [Theory]
        [InlineData(OutputType.Xml, "xml")]
        [InlineData(OutputType.Html, "html")]
        [InlineData(OutputType.Csv, "text")]
        [InlineData(OutputType.Json, "text")]
        [InlineData(OutputType.Text, "text")]
        public void CreateOutputElement_CreatesMethodAttributeCorrectly(OutputType outputType, string expectedValue)
        {
            var builder = new TransformBuilder();
            var element = builder.CreateOutput(outputType, true);
            var methodAttribute = element.Attribute("method");

            Assert.NotNull(methodAttribute);
            Assert.Equal(expectedValue, methodAttribute.Value);
        }
    }
}