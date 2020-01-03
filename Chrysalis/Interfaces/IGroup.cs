using System.Collections.Generic;

namespace Transmogrifier.Chrysalis
{
    /// <summary>
    /// Represents a logical grouping of repeating data points.
    /// </summary>
    public interface IGroup
    {
        /// <summary>
        /// Gets all of the fields in the group.
        /// </summary>
        IEnumerable<IField> Fields { get; }
        /// <summary>
        /// Gets or sets the input context.
        /// </summary>
        string InputContext { get; set; }
        /// <summary>
        /// Gets or sets the output data.
        /// </summary>
        IFieldData OutputData { get; set; }
        /// <summary>
        /// Gets the template match.
        /// </summary>
        string TemplateMatch { get; }
        /// <summary>
        /// Gets the subgroups.
        /// </summary>
        IEnumerable<IGroup> SubGroups { get; }
        /// <summary>
        /// Adds a field.
        /// </summary>
        /// <param name="iField">The field.</param>
        void AddField(IField iField);
        /// <summary>
        /// Adds fields.
        /// </summary>
        /// <param name="paramFields">The parameter fields.</param>
        void AddFields(params IField[] paramFields);
        /// <summary>
        /// Removes a field.
        /// </summary>
        /// <param name="iField">The i field.</param>
        void RemoveField(IField iField);
        /// <summary>
        /// Adds a subgroup.
        /// </summary>
        /// <param name="subGroup">The sub group.</param>
        void AddGroup(ISubGroup subGroup);
        /// <summary>
        /// Removes a subgroup.
        /// </summary>
        /// <param name="subGroup">The sub group.</param>
        void RemoveGroup(ISubGroup subGroup);
    }
}