using System.Collections.Generic;

namespace Transmogrifier.Chrysalis
{
    /// <summary>
    /// Represents any group that is not the root group.
    /// </summary>
    /// <seealso cref="IGroup" />
    public interface ISubGroup : IGroup
    {
        /// <summary>
        /// Gets the key fields.
        /// </summary>
        /// <value>
        /// The key fields.
        /// </value>
        IEnumerable<IField> KeyFields { get; }
        IGroup Parent { get; }

        /// <summary>
        /// Adds the key field.
        /// </summary>
        /// <param name="iField">The i field.</param>
        void AddKeyField(IField iField);
        /// <summary>
        /// Removes the key field.
        /// </summary>
        /// <param name="iField">The i field.</param>
        void RemoveKeyField(IField iField);
    }
}