using Transmogrifier.Chrysalis;
using Xunit;

namespace Transmogrifier.ChrysalisTests
{
    public class FieldTests
    {
        [Fact]
        [Trait("Category", "Serialization")]
        public void Field_Default_Serializes()
        {
            var field = new Field("TestField");
            var element = SerializeUtilities.SerializeToXElement(field);

            Assert.NotNull(element);
            Assert.Equal("Field", element.Name.LocalName);
        }

        [Theory]
        [Trait("Category", "Serialization")]
        [InlineData("Alias")]
        [InlineData("DataType")]
        public void Field_Default_SerializesRequiredElements(string expectedElement)
        {
            var group = new Field("TestField");
            var element = SerializeUtilities.SerializeToXElement(group);

            Assert.Contains(element.Elements(), e => e.Name.LocalName == expectedElement);
        }

        [Theory]
        [Trait("Category", "Serialization")]
        [InlineData("InputData")]
        [InlineData("OutputData")]
        public void Field_Default_DoesNotSerializesOptionalElements(string expectedElement)
        {
            var group = new Field("TestField");
            var element = SerializeUtilities.SerializeToXElement(group);

            Assert.DoesNotContain(element.Elements(), e => e.Name.LocalName == expectedElement);
        }

        [Fact]
        [Trait("Category", "Serialization")]
        public void Field_InputDataPopulated_SerializesInputData()
        {
            var field = new Field("TestField");
            field.InputData = new FieldData("TestFieldData");
            var element = SerializeUtilities.SerializeToXElement(field);

            Assert.Contains(element.Elements(), e => e.Name.LocalName == "InputData");
        }

        [Fact]
        [Trait("Category", "Serialization")]
        public void Field_OutputDataPopulated_SerializesOutputData()
        {
            var field = new Field("TestField");
            field.OutputData = new FieldData("TestFieldData");
            var element = SerializeUtilities.SerializeToXElement(field);

            Assert.Contains(element.Elements(), e => e.Name.LocalName == "OutputData");
        }

        [Fact]
        [Trait("Category", "Equality")]
        public void Equals_SameAliasDifferentFieldData_ReturnsTrue()
        {
            var field1 = new Field("TestField") {InputData = new FieldData("FieldData1")};
            var field2 = new Field("TestField") {InputData = new FieldData("FieldData2")};
            
            Assert.True((bool) field1.Equals(field2));
        }
    }
}