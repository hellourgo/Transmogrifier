namespace Transmogrifier.Chrysalis
{
    /// <inheritdoc />
    public class ChrysalisFactory : IChrysalisFactory
    {
        /// <inheritdoc />
        public T CreateGroup<T>(string templateMatch) where T : IGroup => typeof(T) == typeof(IRootGroup)
            ? (T) CreateRootGroup(templateMatch)
            : (T) CreateSubGroup(templateMatch);

        /// <inheritdoc />
        public IGroup CreateGroup(string templateMatch) => CreateGroup<ISubGroup>(templateMatch);

        /// <inheritdoc />
        public IFieldData
            CreateFieldData(string name, ContentType contentType = ContentType.None, string path = null) =>
            new FieldData(name, contentType, path);

        /// <inheritdoc />
        public IField CreateField(string alias, IFieldData inputData = null, IFieldData outputData = null) =>
            new Field(alias, inputData, outputData);

        /// <inheritdoc />
        public IChrysalis CreateChrysalis() => new Chrysalis();

        internal IRootGroup CreateRootGroup(string templateMatch) => new RootGroup(templateMatch);
        internal ISubGroup CreateSubGroup(string templateMatch) => new SubGroup(templateMatch);
    }
}