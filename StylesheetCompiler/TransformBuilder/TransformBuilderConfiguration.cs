using Transmogrifier.Chrysalis;

namespace Transmogrifier.StylesheetCompiler.TransformBuilder
{
    public class TransformBuilderConfiguration
    {
        public OutputType OutputType { get; set; } = OutputType.Xml;
        public bool FormatXml { get; set; } = true;
        public bool IgnoreWhitespace { get; set; } = true;
    }
}