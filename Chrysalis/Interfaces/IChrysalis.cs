using System.Collections.Generic;

namespace Transmogrifier.Chrysalis
{
    /// <summary>
    /// Represents a container for groups.
    /// </summary>
    public interface IChrysalis
    {
        /// <summary>
        /// Gets the root groups.
        /// </summary>
        /// <value>
        /// The root groups.
        /// </value>
        IEnumerable<IRootGroup> RootGroups { get; }

        /// <summary>
        /// Adds a root group.
        /// </summary>
        /// <param name="group">The group.</param>
        void AddRootGroup(IRootGroup group);
    }
}