using System.Xml.Linq;
using Transmogrifier.Chrysalis;
using Transmogrifier.Xslt;

namespace Transmogrifier.StylesheetCompiler.TransformBuilder
{
    public class TransformBuilder
    {
        private readonly TransformBuilderConfiguration configuration;
        private XElement rootElement;
        private readonly XsltFactory xslt;

        public TransformBuilder(TransformBuilderConfiguration configuration = null, XsltFactory xslt = null)
        {
            this.xslt = xslt ?? new XsltFactory();
            this.configuration = configuration ?? new TransformBuilderConfiguration();
            Initialize();
        }

        public TransformBuilder Append(object content) => Append(new[] {content});

        public TransformBuilder Append(params object[] content)
        {
            rootElement.Add(content);
            return this;
        }

        public XDocument Build() => new XDocument(rootElement);

        public XElement CreateTemplateCopy()
        {
            var copyArg = "@*|node()";
            var templateElement = xslt.Template(copyArg);
            var copyElement = xslt.Copy();

            copyElement.Add(xslt.ApplyTemplates(copyArg));
            templateElement.Add(copyElement);
            
            return templateElement;
        }

        public void Clear()
        {
            rootElement = xslt.Stylesheet();
        }

        internal XElement CreateOutput(OutputType outputType, bool formatXml) =>
            xslt.Output(GetOutputMethod(outputType), formatXml);

        internal void Initialize()
        {
            Clear();
            if (configuration.IgnoreWhitespace)
                Append(xslt.StripSpace("*"));
            Append(CreateOutput(configuration.OutputType, configuration.FormatXml));
            Append(CreateTemplateCopy());
        }

        private static OutputMethod GetOutputMethod(OutputType outputType)
        {
            switch (outputType)
            {
                case OutputType.Xml:
                    return OutputMethod.Xml;
                case OutputType.Html:
                    return OutputMethod.Html;
                default:
                    return OutputMethod.Text;
            }
        }
    }
}