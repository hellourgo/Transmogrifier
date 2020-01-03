using Transmogrifier.Chrysalis;
using Xunit;

namespace Transmogrifier.ChrysalisTests
{
    public class FieldDataTests
    {
        [Fact]
        [Trait("Category", "Equality")]
        public void Equals_AllPropertiesEqual_ReturnsTrue()
        {
            var fieldData1 = new FieldData("FieldData1", ContentType.Element, "Path1");
            var fieldData2 = new FieldData("FieldData1", ContentType.Element, "Path1");

            Assert.NotSame(fieldData1, fieldData2);
            Assert.True(fieldData1.Equals(fieldData2));
        }

        [Fact]
        [Trait("Category", "Equality")]
        public void Equals_AnyPropertiesDifferent_ReturnsFalse()
        {
            var fieldData1 = new FieldData("FieldData1", ContentType.Element, "Path1");
            var fieldData2 = new FieldData("FieldData1", ContentType.Attribute, "Path1");

            Assert.NotSame(fieldData1, fieldData2);
            Assert.False(fieldData1.Equals(fieldData2));
        }

        [Theory]
        [Trait("Category", "Serialization")]
        [InlineData("Path")]
        public void FieldData_Default_DoesNotSerializesOptionalElements(string expectedElement)
        {
            var fieldData = new FieldData("TestFieldData");
            var element = SerializeUtilities.SerializeToXElement(fieldData);

            Assert.DoesNotContain(element.Elements(), e => e.Name.LocalName == expectedElement);
        }

        [Fact]
        [Trait("Category", "Serialization")]
        public void FieldData_Default_Serializes()
        {
            var fieldData = new FieldData("TestFieldData");
            var element = SerializeUtilities.SerializeToXElement(fieldData);

            Assert.NotNull(element);
            Assert.Equal("FieldData", element.Name.LocalName);
        }

        [Theory]
        [Trait("Category", "Serialization")]
        [InlineData("Name")]
        [InlineData("ContentType")]
        public void FieldData_Default_SerializesRequiredElements(string expectedElement)
        {
            var fieldData = new FieldData("TestFieldData");
            var element = SerializeUtilities.SerializeToXElement(fieldData);

            Assert.Contains(element.Elements(), e => e.Name.LocalName == expectedElement);
        }

        [Fact]
        [Trait("Category", "Serialization")]
        public void FieldData_PathPopulated_SerializesPath()
        {
            var fieldData = new FieldData("TestFieldData", path: "SomePath");
            var element = SerializeUtilities.SerializeToXElement(fieldData);

            Assert.Contains(element.Elements(), e => e.Name.LocalName == "Path");
        }

        [Theory]
        [InlineData("Test", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void HasPath_StringInput_ReturnsCorrectly(string input, bool expectedResult)
        {
            var fieldData = new FieldData("TestFieldData", path: input);

            Assert.Equal(expectedResult, fieldData.HasPath());
        }
    }
}