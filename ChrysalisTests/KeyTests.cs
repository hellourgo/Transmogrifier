using Transmogrifier.Chrysalis;
using Xunit;

namespace Transmogrifier.ChrysalisTests
{
    public class KeyTests
    {
        [Fact]
        [Trait("Category", "Serialization")]
        public void Key_Default_Serializes()
        {
            var key = new Key();
            var element = SerializeUtilities.SerializeToXElement(key);

            Assert.NotNull(element);
            Assert.Equal("Key", element.Name.LocalName);
        }

        [Theory]
        [Trait("Category", "Serialization")]
        [InlineData("KeyFields")]
        public void Key_Default_SerializesRequiredElements(string expectedElement)
        {
            var key = new Key();
            var element = SerializeUtilities.SerializeToXElement(key);

            Assert.Contains(element.Elements(), e => e.Name.LocalName == expectedElement);
        }

        [Fact]
        public void Constructor_Null_DoesNothing()
        {
            var key = new Key(null);
            Assert.Empty(key.KeyFields);
        }

        [Fact]
        public void Constructor_Single_AddsKeyField()
        {
            var field = new Field("TestField");
            var key = new Key(field);
            Assert.Contains<Field>(field, key.KeyFields);
        }

        [Fact]
        public void Constructor_Multiple_AddsKeyField()
        {
            var field1 = new Field("TestField1");
            var field2 = new Field("TestField2");
            var key = new Key(field1, field2);
            Assert.Contains(field1, key.KeyFields);
            Assert.Contains(field2, key.KeyFields);
        }

        [Fact]
        [Trait("Category", "Equality")]
        public void Equals_SameKeyFieldsDifferentOrder_ReturnsTrue()
        {
            var field1 = new Field("TestField1");
            var field2 = new Field("TestField2");

            var key1 = new Key(field1, field2);
            var key2 = new Key(field2, field1);

            Assert.True(key1.Equals(key2));
        }

        [Fact]
        [Trait("Category", "Equality")]
        public void Equals_SameKeyFieldsSameOrder_ReturnsTrue()
        {
            var field1 = new Field("TestField1");
            var field2 = new Field("TestField2");

            var key1 = new Key(field1, field2);
            var key2 = new Key(field1, field2);

            Assert.True(key1.Equals(key2));
        }
    }
}