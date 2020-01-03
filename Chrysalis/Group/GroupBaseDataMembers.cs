using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Transmogrifier.Chrysalis
{
    [DataContract(Name = "Group", Namespace = "https://www.transmogrifier.com/chrysalis")]
    public abstract partial class GroupBase
    {
        protected GroupBase()
        {
        }

        protected GroupBase(string templateMatch) => this.templateMatch = templateMatch;

        [DataMember(Name = "TemplateMatch")]
        private string templateMatch;

        [DataMember(Name = "Fields")]
        private List<Field> fields = new List<Field>();

        [DataMember(Name = "InputContext", EmitDefaultValue = false)]
        private string inputContext;

        [DataMember(Name = "OutputData")]
        private FieldData outputData;

        [DataMember(Name = "Groups", EmitDefaultValue = false)]
        private List<GroupBase> childGroups = new List<GroupBase>();
    }
}