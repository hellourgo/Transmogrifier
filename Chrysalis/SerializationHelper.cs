using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Transmogrifier.Chrysalis
{
    /// <summary>
    /// Provides methods to serialize and deserialize Chrysalis files.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBeInternal")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class SerializationHelper
    {
        private static readonly DataContractSerializer serializer = new DataContractSerializer(typeof(Chrysalis), GetSettings());

        private static DataContractSerializerSettings GetSettings() =>
            new DataContractSerializerSettings
            {
                PreserveObjectReferences = true,
            };

        /// <summary>
        /// Serializes the specified chrysalis.
        /// </summary>
        /// <param name="chrysalis">The chrysalis.</param>
        /// <param name="stream">The stream.</param>
        public static void Serialize(IChrysalis chrysalis, Stream stream)
        {
            serializer.WriteObject(stream, chrysalis);
        }

        /// <summary>
        /// Serializes the specified chrysalis.
        /// </summary>
        /// <param name="chrysalis">The chrysalis.</param>
        /// <param name="writer">The writer.</param>
        public static void Serialize(IChrysalis chrysalis, XmlTextWriter writer)
        {
            serializer.WriteObject(writer, chrysalis);
        }

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public static IChrysalis Deserialize(Stream stream) => (IChrysalis)serializer.ReadObject(stream);

        /// <summary>
        /// Deserializes the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public static IChrysalis Deserialize(XmlReader reader) => (IChrysalis)serializer.ReadObject(reader);
    }
}