using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Transmogrifier.Chrysalis;
using Transmogrifier.StylesheetCompiler.StylesheetGenerator;
using Transmogrifier.Xslt;
using Xunit;

namespace Transmogrifier.StylesheetCompilerTests
{
    public class StylesheetGeneratorTests
    {

        [Fact]
        [Trait("Category", "Integration")]
        public void CreateStylesheets_TestChrysalis_CreatesCorrectStylesheet()
        {
            var chrysalis = ChrysalisMockFactory.CreateChrysalis();
            var generator = new StylesheetGenerator();
            var document = generator.CreateStylesheets(chrysalis).First();

            Assert.IsType<XsltElement>(document.Root);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void CreateStylesheets_TestChrysalis_TransformsInputCorrectly()
        {
            var chrysalis = ChrysalisMockFactory.CreateChrysalis();
            var generator = new StylesheetGenerator();
            var document = generator.CreateStylesheets(chrysalis).First();
            
            //WriteToFile(stylesheet);
            using (var documentReader = document.CreateReader())
            using (var inputReader = XmlReader.Create("TestCsvInput.xml"))
            using (var stream = new MemoryStream())
            using (var outputWriter = XmlWriter.Create(stream))
            {
                var transform = new XslCompiledTransform();

                transform.Load(documentReader);
                transform.Transform(inputReader, outputWriter);
            }
        }

        [Fact]
        public void GetKeyElement_GroupWithKeyFields_ReturnsXsltKeyElement()
        {
            var testKeyField = ChrysalisMockFactory.MockIField("TestKeyField");
            testKeyField.InputData = ChrysalisMockFactory.MockIFieldData("TestInputFieldData");

            var testGroup = ChrysalisMockFactory.MockISubGroup("TestTemplateMatch", testKeyField);
            testGroup.OutputData = ChrysalisMockFactory.MockIFieldData("TestOutputData");

            var generator = new StylesheetGenerator();
            var keyElement = generator.GetKeyElement(testGroup);
            Assert.Equal(XsltElementType.Key, keyElement.XsltElementType);
        }

        [Fact]
        public void GetKeyElement_GroupWithoutKeyFields_ReturnsNull()
        {
            var generator = new StylesheetGenerator();
            var testGroup = ChrysalisMockFactory.MockISubGroup("TestTemplateMatch");
            testGroup.OutputData = ChrysalisMockFactory.MockIFieldData("TestOutputData");
            var keyElement = generator.GetKeyElement(testGroup);

            Assert.Null(keyElement);
        }

        [Theory]
        [InlineData(ContentType.Variable, XsltElementType.Variable)]
        [InlineData(ContentType.Attribute, XsltElementType.Attribute)]
        [InlineData(ContentType.Element, XsltElementType.Element)]
        [InlineData(ContentType.Text, XsltElementType.Text)]
        [InlineData(ContentType.Number, XsltElementType.Number)]
        [InlineData(ContentType.Calculation, XsltElementType.ValueOf)]
        [InlineData(ContentType.Aggregation, XsltElementType.ValueOf)]
        public void GetOutputElement_InlineData_ReturnsExpectedXsltElement(ContentType fieldContentType, XsltElementType expectedXsltElementType)
        {
            var generator = new StylesheetGenerator();
            var testFieldData = ChrysalisMockFactory.MockIFieldData("TestFieldData", fieldContentType);

            var outputElement = generator.GetOutputElement(testFieldData);
            Assert.Equal(expectedXsltElementType, outputElement.XsltElementType);
        }

        [Fact]
        public void GetOutputElement_ContentTypeNone_ReturnsNull()
        {
            var generator = new StylesheetGenerator();
            var testFieldData = ChrysalisMockFactory.MockIFieldData("TestFieldData", ContentType.None);

            var outputElement = generator.GetOutputElement(testFieldData);
            Assert.Null(outputElement);
        }

        private void WriteToFile(XDocument doc)
        {
            using (var writer = new XmlTextWriter("transform.xslt", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                doc.WriteTo(writer);
            }
        }
    }
}