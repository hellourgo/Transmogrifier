using System.Collections.Generic;
using System.Linq;

namespace Transmogrifier.Chrysalis
{

    public abstract partial class GroupBase : IGroup
    {
        public string TemplateMatch { get => templateMatch; private set => templateMatch = value; }

        public string InputContext { get => inputContext; set => inputContext = value; }

        public IFieldData OutputData { get => outputData; set => outputData = (FieldData)value; }

        public IEnumerable<IField> Fields => fields.AsReadOnly();

        internal IEnumerable<GroupBase> ChildGroups => childGroups.AsReadOnly();

        public IEnumerable<IGroup> SubGroups => childGroups.AsReadOnly();

        protected GroupBase ParentGroup { get; set; }

        public void AddField(IField iField)
        {
            if (iField is Field field && !fields.Contains(field))
                fields.Add(field);
        }

        public void AddFields(params IField[] paramFields)
        {
            paramFields?.ToList().ForEach(AddField);
        }

        public virtual void RemoveField(IField iField)
        {
            if (iField is Field field && fields.Contains(field))
                fields.Remove(field);
        }

        public void AddGroup(ISubGroup subGroup)
        {
            var groupBase = (GroupBase)subGroup;
            if (groupBase.ParentGroup != null && !Equals(groupBase.ParentGroup, this)) groupBase.ParentGroup.RemoveGroup(subGroup);
            groupBase.ParentGroup = this;

            childGroups.Add(groupBase);
        }

        public void RemoveGroup(ISubGroup subGroup)
        {
            var groupBase = (GroupBase)subGroup;

            if (!childGroups.Contains(groupBase)) return;
            groupBase.ParentGroup = null;
            childGroups.Remove(groupBase);
        }

        internal IEnumerable<GroupBase> Ancestors() => GetAncestors(false);

        internal IEnumerable<GroupBase> AncestorsAndSelf() => GetAncestors(true);

        private IEnumerable<GroupBase> GetAncestors(bool self)
        {
            var group = self ? this : ParentGroup;
            while (group != null)
            {
                yield return group;

                group = group.ParentGroup;
            }
        }

        internal IEnumerable<GroupBase> Descendants() => GetDescendants(false);

        private IEnumerable<GroupBase> GetDescendants(bool self)
        {
            if (self) yield return this;
        }
    }
}