using System.Linq;
using System.Xml.Linq;
using Transmogrifier.Xslt;
using Xunit;

namespace Transmogrifier.XsltTests
{
    public class XsltFactoryTests
    {
        [Fact]
        public void Stylesheet_CreatesElement()
        {
            var xslt = new XsltFactory();
            var stylesheet = xslt.Stylesheet();
            Assert.Equal("stylesheet", stylesheet.Name.LocalName);
            Assert.Contains(stylesheet.Attributes(), a => a.Name == "version" && a.Value=="1.0");
            Assert.Contains(stylesheet.Attributes(), a => a.Name == XNamespace.Xmlns + "xsl" && a.Value== XsltElement.XslNamespace.NamespaceName);
        }

        [Fact]
        public void StripSpace_String_CreatesElement()
        {
            var xslt = new XsltFactory();
            var elements = "test";
            var stripSpace = xslt.StripSpace(elements);
            Assert.Equal("strip-space", stripSpace.Name.LocalName);
            Assert.Contains(stripSpace.Attributes(), a => a.Name == "elements" && a.Value == elements);
        }

        [Fact]
        public void Attribute_String_CreatesElement()
        {
            var xslt = new XsltFactory();
            var name = "testName";
            var attribute = xslt.Attribute(name);
            Assert.Equal("attribute", attribute.Name.LocalName);
            Assert.Contains(attribute.Attributes(), a => a.Name == "name" && a.Value == name);
        }

        [Fact]
        public void Element_String_CreatesElement()
        {
            var xslt = new XsltFactory();
            var name = "testName";
            var element = xslt.Element(name);
            Assert.Equal("element", element.Name.LocalName);
            Assert.Contains(element.Attributes(), a => a.Name == "name" && a.Value == name);
        }

        [Fact]
        public void Variable_String_CreatesElement()
        {
            var xslt = new XsltFactory();
            var name = "testName";
            var variable = xslt.Variable(name);
            Assert.Equal("variable", variable.Name.LocalName);
            Assert.Contains(variable.Attributes(), a => a.Name == "name" && a.Value == name);
        }

        [Fact]
        public void ValueOf_String_CreatesElement()
        {
            var xslt = new XsltFactory();
            var select = "testSelect";
            var valueOf = xslt.ValueOf(select);
            Assert.Equal("value-of", valueOf.Name.LocalName);
            Assert.Contains(valueOf.Attributes(), a => a.Name == "select" && a.Value == select);
        }

        [Fact]
        public void Key_String_CreatesElement()
        {
            var xslt = new XsltFactory();
            var name = "testName";
            var match = "testMatch";
            var use = "testUse";
            var key = xslt.Key(name, match, use);

            Assert.Equal("key", key.Name.LocalName);
            Assert.Contains(key.Attributes(), a => a.Name == "name" && a.Value == name);
            Assert.Contains(key.Attributes(), a => a.Name == "match" && a.Value == match);
            Assert.Contains(key.Attributes(), a => a.Name == "use" && a.Value == use);
        }

        [Fact]
        public void Text_None_CreatesElement()
        {
            var xslt = new XsltFactory();
            var text = xslt.Text();

            Assert.Equal("text", text.Name.LocalName);
            Assert.False(text.Attributes().Any());
        }

        [Fact]
        public void Text_String_CreatesElementWithData()
        {
            var data = "TestData";
            var xslt = new XsltFactory();
            var text = xslt.Text(data);

            Assert.Equal("text", text.Name.LocalName);
            Assert.False(text.Attributes().Any());
            Assert.Equal(data, text.Value);
        }

        [Fact]
        public void Number_None_CreatesElement()
        {
            var xslt = new XsltFactory();
            var text = xslt.Number();

            Assert.Equal("number", text.Name.LocalName);
            Assert.False(text.Attributes().Any());
        }

        [Fact]
        public void Copy_None_CreatesElement()
        {
            var xslt = new XsltFactory();
            var text = xslt.Copy();

            Assert.Equal("copy", text.Name.LocalName);
            Assert.False(text.Attributes().Any());
        }

        [Theory]
        [InlineData(OutputMethod.Xml)]
        [InlineData(OutputMethod.Html)]
        [InlineData(OutputMethod.Text)]
        public void Output_OutputMethod_CreatesElementWithMethod(OutputMethod outputMethod)
        {
            var xslt = new XsltFactory();
            var method = outputMethod.ToString().ToLower();
            
            var output = xslt.Output(outputMethod);

            Assert.Equal("output", output.Name.LocalName);
            Assert.Contains(output.Attributes(), a => a.Name == "method" && a.Value == method);
            Assert.DoesNotContain(output.Attributes(), a => a.Name == "indent");
        }

        [Fact]
        public void Output_DefaultOutputMethodAndIndent_CreatesElementWithDefaultMethodAndIndent()
        {
            var xslt = new XsltFactory();

            var output = xslt.Output(indent:true);

            Assert.Equal("output", output.Name.LocalName);
            Assert.Contains(output.Attributes(), a => a.Name == "method" && a.Value == OutputMethod.Xml.ToString().ToLower());
            Assert.Contains(output.Attributes(), a => a.Name == "indent" && a.Value == "yes");
        }

        [Fact]
        public void ApplyTemplates_SelectStringOnly_CreatesElementWithoutModeAttribute()
        {
            var xslt = new XsltFactory();
            var select = "testSelect";
            var applyTemplates = xslt.ApplyTemplates(select);

            Assert.Equal("apply-templates", applyTemplates.Name.LocalName);
            Assert.Contains(applyTemplates.Attributes(), a => a.Name == "select" && a.Value == select);
            Assert.DoesNotContain(applyTemplates.Attributes(), a => a.Name == "mode");
        }

        [Fact]
        public void ApplyTemplates_SelectAndMode_CreatesElementWithModeAttribute()
        {
            var xslt = new XsltFactory();
            var select = "testSelect";
            var mode = "testMode";
            var applyTemplates = xslt.ApplyTemplates(select, mode);

            Assert.Equal("apply-templates", applyTemplates.Name.LocalName);
            Assert.Contains(applyTemplates.Attributes(), a => a.Name == "select" && a.Value == select);
            Assert.Contains(applyTemplates.Attributes(), a => a.Name == "mode" && a.Value == mode);
        }

        [Fact]
        public void Template_MatchStringOnly_CreatesElementWithoutModeAttribute()
        {
            var xslt = new XsltFactory();
            var match = "testMatch";
            var template = xslt.Template(match);

            Assert.Equal("template", template.Name.LocalName);
            Assert.Contains(template.Attributes(), a => a.Name == "match" && a.Value == match);
            Assert.DoesNotContain(template.Attributes(), a => a.Name == "mode");
        }

        [Fact]
        public void Template_MatchAndMode_CreatesElementWithModeAttribute()
        {
            var xslt = new XsltFactory();
            var match = "testMatch";
            var mode = "testMode";
            var template = xslt.Template(match, mode);

            Assert.Equal("template", template.Name.LocalName);
            Assert.Contains(template.Attributes(), a => a.Name == "match" && a.Value == match);
            Assert.Contains(template.Attributes(), a => a.Name == "mode" && a.Value == mode);
        }
    }
}
