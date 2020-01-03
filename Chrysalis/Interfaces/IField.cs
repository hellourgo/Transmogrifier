namespace Transmogrifier.Chrysalis
{
    /// <summary>
    /// Represents a single data shared data point between the input and output files.
    /// </summary>
    public interface IField
    {
        /// <summary>
        /// The user-friendly name for the field.
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        string DataType { get; set; }
        /// <summary>
        /// Gets or sets the input field data.
        /// </summary>
        /// <value>
        /// The input data.
        /// </value>
        IFieldData InputData { get; set; }
        /// <summary>
        /// Gets or sets the output field data.
        /// </summary>
        /// <value>
        /// The output data.
        /// </value>
        IFieldData OutputData { get; set; }
    }
}