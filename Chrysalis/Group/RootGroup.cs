using System.Runtime.Serialization;

namespace Transmogrifier.Chrysalis
{
    [DataContract(Namespace = "https://www.transmogrifier.com/chrysalis")]
    public class RootGroup : GroupBase, IRootGroup
    {
        public RootGroup(string templateMatch) : base(templateMatch)
        {
        }

        [DataMember(EmitDefaultValue = false)]
        public OutputType OutputType { get; set; } = OutputType.Xml;

    }
}