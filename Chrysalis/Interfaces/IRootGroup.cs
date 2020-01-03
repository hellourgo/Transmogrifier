namespace Transmogrifier.Chrysalis
{
    /// <summary>
    /// Represents the root group of a Chrysalis. 
    /// </summary>
    /// <seealso cref="IGroup" />
    public interface IRootGroup : IGroup
    {
        /// <summary>
        /// Gets or sets the type of the output.
        /// </summary>
        OutputType OutputType { get; set; }
    }
}