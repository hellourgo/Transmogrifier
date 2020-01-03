using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Transmogrifier.ChrysalisTests
{
    public static class SerializeUtilities
    {
        public static XElement SerializeToXElement(object value)
        {
            var serializer = new DataContractSerializer(value.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, value);
                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    var serializedElement = XElement.Load(reader);
                    return serializedElement;
                }
            }
        }
    }
}