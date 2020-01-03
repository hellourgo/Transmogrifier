namespace Transmogrifier.Chrysalis
{
    /// <summary>
    /// Represents a data point on one side of the transformation.
    /// </summary>
    public interface IFieldData
    {
        /// <summary>
        /// What type of content the data is stored as in the file.
        /// </summary>
        ContentType ContentType { get; set; }
        /// <summary>
        /// Represents the name of the container the data is stored in or, if the data is a constant value, the value of the constant.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The path to the data point under the current group.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Determines whether this instance has a path.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has path; otherwise, <c>false</c>.
        /// </returns>
        bool HasPath();
    }
}