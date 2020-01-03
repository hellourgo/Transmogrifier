using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Transmogrifier.Chrysalis
{
    [DataContract(Namespace = "https://www.transmogrifier.com/chrysalis")]
    public class SubGroup : GroupBase, ISubGroup
    {
        public SubGroup(string templateMatch) : base(templateMatch)
        {
        }

        [DataMember(Name = "Key")]
        internal Key Key = new Key();

        public IEnumerable<IField> KeyFields => GetKeyFields().AsReadOnly();
        public IGroup Parent => ParentGroup;

        public void AddKeyField(IField iField)
        {
            if (iField is Field field && !KeyFields.Contains(field))
                Key.KeyFields.Add(field);
            AddField(iField);
        }

        public void RemoveKeyField(IField iField)
        {
            if (iField is Field field && Key.KeyFields.Contains(field))
                Key.KeyFields.Remove(field);
        }

        public override void RemoveField(IField field)
        {
            RemoveKeyField(field);
            base.RemoveField(field);
        }
        
        private List<IField> GetKeyFields()
        {
            var keyFields = new List<IField>();
            if (Parent is ISubGroup subGroup)
                keyFields.AddRange(subGroup.KeyFields);
            keyFields.AddRange(Key.KeyFields);
            return keyFields;
        }
    }
}