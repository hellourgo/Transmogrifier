using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Transmogrifier.Chrysalis;
using Xunit;

namespace Transmogrifier.ChrysalisTests
{
    public class ChrysalisTests
    {
        [Fact]
        [Trait("Category", "Serialization")]
        public void Serialize_MemoryStream_Serializes()
        {
            var chrysalis = MockChrysalisFactory.CreateChrysalis();
            using (var stream = new MemoryStream())
            {
                SerializationHelper.Serialize(chrysalis, stream);
            }
        }

        [Fact]
        [Trait("Category", "Serialization")]
        public void Deserialize_MemoryStream_Deserializes()
        {
            var chrysalis = MockChrysalisFactory.CreateChrysalis();
            using (var stream = new MemoryStream())
            {
                SerializationHelper.Serialize(chrysalis, stream);
                stream.Position = 0;
                var inputChrysalis = SerializationHelper.Deserialize(stream);
                Assert.True(chrysalis.RootGroups.First().Equals(inputChrysalis.RootGroups.First()));
            }
        }


        [Fact]
        [Trait("Category", "Serialization")]
        public void Serialize_XmlTextWriter_Serializes()
        {
            var chrysalis = MockChrysalisFactory.CreateChrysalis();
            using (var stream = new MemoryStream())
            using (var textWriter = new XmlTextWriter(stream, Encoding.UTF8))
            {
                textWriter.Formatting = Formatting.Indented;
                SerializationHelper.Serialize(chrysalis, textWriter);
            }
        }

        [Fact]
        [Trait("Category", "Serialization")]
        public void Deserialize_XmlReader_Deserializes()
        {
            var chrysalis = MockChrysalisFactory.CreateChrysalis();
            IChrysalis inputChrysalis;
            using (var stream = new MemoryStream())
            {
                SerializationHelper.Serialize(chrysalis, stream);
                stream.Position = 0;
                using (var reader = XmlReader.Create(stream))
                {
                    inputChrysalis = SerializationHelper.Deserialize(reader);
                }
            }

            Assert.True(chrysalis.RootGroups.First().Equals(inputChrysalis.RootGroups.First()));
        }
    }
}