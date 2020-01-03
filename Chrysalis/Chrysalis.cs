using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Transmogrifier.Chrysalis
{
    [DataContract(Namespace = "https://www.transmogrifier.com/chrysalis"), KnownType(typeof(SubGroup))]
    internal class Chrysalis : IChrysalis
    {
        [DataMember(Name = "RootGroups")]
        private List<RootGroup> rootGroups = new List<RootGroup>();

        public IEnumerable<IRootGroup> RootGroups => rootGroups;

        public void AddRootGroup(IRootGroup group)
        {
            if (group is RootGroup rootGroup && !rootGroups.Contains(rootGroup))
                rootGroups.Add(rootGroup);
        }
    }
}