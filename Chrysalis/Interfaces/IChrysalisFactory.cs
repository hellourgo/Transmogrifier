namespace Transmogrifier.Chrysalis
{
    /// <summary>
    /// Provides methods to create Chrysalis file nodes.
    /// </summary>
    public interface IChrysalisFactory
    {
        /// <summary>
        /// Creates a chrysalis object.
        /// </summary>
        IChrysalis CreateChrysalis();
        /// <summary>
        /// Creates a field object.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="inputData">The input data.</param>
        /// <param name="outputData">The output data.</param>
        IField CreateField(string alias, IFieldData inputData = null, IFieldData outputData = null);
        /// <summary>
        /// Creates a field data object.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="path">The path.</param>
        IFieldData CreateFieldData(string name, ContentType contentType = ContentType.None, string path = null);
        /// <summary>
        /// Creates a generic <see cref="IGroup"/>.
        /// </summary>
        /// <param name="templateMatch">The template match.</param>
        IGroup CreateGroup(string templateMatch);
        /// <summary>
        ///     Creates a group of the provided type <typeparamref name="T" />, which needs to inherit from <see cref="IGroup" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="templateMatch">The template match.</param>
        T CreateGroup<T>(string templateMatch) where T : IGroup;
    }
}