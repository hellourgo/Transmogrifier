using System.ComponentModel;
using Transmogrifier.Xslt;
using Xunit;

namespace Transmogrifier.XsltTests
{
    public class EnumExtensionsTests
    {
        private enum TestEnumWithValues
        {
            [Description("Has Description")]
            HasDescription,
            DoesNotHaveDescription
        }

        private enum TestEnumWithoutValues
        {

        }
        
        [Fact]
        public void GetDescription_DescriptionExists_ReturnsDescription()
        {
            var description = TestEnumWithValues.HasDescription.GetDescription();
            Assert.Equal("Has Description", description);
        }

        [Fact]
        public void GetDescription_DescriptionDoesNotExist_ReturnsEnumToString()
        {
            var toString = TestEnumWithValues.DoesNotHaveDescription.ToString();
            var description = TestEnumWithValues.DoesNotHaveDescription.GetDescription();
            Assert.Equal(toString, description);
        }

        [Fact]
        public void GetDescription_EnumIsEmpty_ReturnsZeroString()
        {
            var emptyEnum = new TestEnumWithoutValues();
            var description = emptyEnum.GetDescription();
            Assert.Equal("0", description);
        }

        
    }
}