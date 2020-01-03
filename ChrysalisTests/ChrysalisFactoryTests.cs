using Transmogrifier.Chrysalis;
using Xunit;

namespace Transmogrifier.ChrysalisTests
{
    public class ChrysalisFactoryTests
    {
        [Fact]
        public void CreateSubGroup_Default_ReturnsISubGroup()
        {
            var factory = new ChrysalisFactory();
            var subGroup = factory.CreateSubGroup("TestSubGroup");

            Assert.IsAssignableFrom<ISubGroup>(subGroup);
        }

        [Fact]
        public void CreateRootGroup_Default_ReturnsIRootGroup()
        {
            var factory = new ChrysalisFactory();
            var rootGroup = factory.CreateRootGroup("TestRootGroup");

            Assert.IsAssignableFrom<IRootGroup>(rootGroup);
        }

        [Fact]
        public void CreateGroup_IRootGroup_ReturnsIRootGroup()
        {
            var factory = new ChrysalisFactory();
            var rootGroup = factory.CreateGroup<IRootGroup>("TestRootGroup");

            Assert.IsAssignableFrom<IRootGroup>(rootGroup);
        }

        [Fact]
        public void CreateGroup_ISubGroup_ReturnsISubGroup()
        {
            var factory = new ChrysalisFactory();
            var rootGroup = factory.CreateGroup<ISubGroup>("TestSubGroup");
            Assert.IsAssignableFrom<ISubGroup>(rootGroup);
        }

        [Fact]
        public void CreateGroup_NoTypeParameter_ReturnsISubGroup()
        {
            var factory = new ChrysalisFactory();
            var rootGroup = factory.CreateGroup("TestSubGroup");
            Assert.IsAssignableFrom<ISubGroup>(rootGroup);
        }

        [Fact]
        public void CreateFieldData_Default_ReturnsIFieldData()
        {
            var factory = new ChrysalisFactory();
            var fieldData = factory.CreateFieldData("TestFieldData");

            Assert.IsAssignableFrom<IFieldData>(fieldData);
        }

        [Fact]
        public void CreateFieldData_Default_SetsProperties()
        {
            var factory = new ChrysalisFactory();
            var name = "TestFieldData";
            var fieldData = factory.CreateFieldData(name);

            Assert.Equal(name, fieldData.Name);
            Assert.Equal(ContentType.None, fieldData.ContentType);
            Assert.Null(fieldData.Path);
        }

        [Fact]
        public void CreateFieldData_ProvideOptionalParameters_SetsProperties()
        {
            var factory = new ChrysalisFactory();
            var name = "TestFieldData";
            var contentType = ContentType.Attribute;
            var path = "TestFieldDataPath";
            var fieldData = factory.CreateFieldData(name, contentType, path);
            

            Assert.Equal(name, fieldData.Name);
            Assert.Equal(contentType, fieldData.ContentType);
            Assert.Equal(path, fieldData.Path);
        }

        [Fact]
        public void CreateField_Default_ReturnsIField()
        {
            var factory = new ChrysalisFactory();
            var field = factory.CreateField("TestAlias");

            Assert.IsAssignableFrom<IField>(field);
        }

        [Fact]
        public void CreateChrysalis_Default_ReturnsIChrysalis()
        {
            var factory = new ChrysalisFactory();
            var chrysalis = factory.CreateChrysalis();

            Assert.IsAssignableFrom<IChrysalis>(chrysalis);
        }
    }
}