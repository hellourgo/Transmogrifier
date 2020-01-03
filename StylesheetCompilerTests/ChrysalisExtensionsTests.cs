using System.Collections.Generic;
using System.Text.RegularExpressions;
using Moq;
using Transmogrifier.Chrysalis;
using Transmogrifier.StylesheetCompiler.StylesheetGenerator;
using Xunit;

namespace Transmogrifier.StylesheetCompilerTests
{
    public class ChrysalisExtensionsTests
    {
        [Fact]
        public void GetFullPath_NullFieldData_ReturnsEmptyString()
        {
            var fullPath = ChrysalisExtensions.GetFullPath(null);
            Assert.Equal(string.Empty, fullPath);
        }

        [Fact]
        public void GetFullPath_NullName_ReturnsEmptyString()
        {
            var mockIFieldData = new Mock<IFieldData>();
            
            var fullPath = mockIFieldData.Object.GetFullPath();
            Assert.Equal(string.Empty, fullPath);
        }

        [Fact]
        public void GetFullPath_ValidNameNoPathElement_ReturnsName()
        {
            var name = "TestName";
            var mockIFieldData = new Mock<IFieldData>();
            mockIFieldData.Setup(f => f.Name).Returns(name);
            mockIFieldData.Setup(f => f.ContentType).Returns(ContentType.Element);
            var fieldData = mockIFieldData.Object;
            var fullPath = fieldData.GetFullPath();

            Assert.Equal(name, fullPath);
        }

        [Fact]
        public void GetFullPath_ValidNameNoPathAttribute_ReturnsNameWithAt()
        {
            var name = "TestName";
            var mockIFieldData = new Mock<IFieldData>();
            mockIFieldData.Setup(f => f.Name).Returns(name);
            mockIFieldData.Setup(f => f.ContentType).Returns(ContentType.Attribute);
            var fieldData = mockIFieldData.Object;
            var fullPath = fieldData.GetFullPath();

            Assert.Equal("@"+name, fullPath);
        }

        [Fact]
        public void GetFullPath_ValidNameValidPathElement_ReturnsName()
        {
            var name = "TestName";
            var testPath = "TestPath";
            var mockIFieldData = new Mock<IFieldData>();
            mockIFieldData.Setup(f => f.Name).Returns(name);
            mockIFieldData.Setup(f => f.Path).Returns(testPath);
            mockIFieldData.Setup(f => f.ContentType).Returns(ContentType.Element);
            mockIFieldData.Setup(f => f.HasPath()).Returns(true);
            var fieldData = mockIFieldData.Object;
            var fullPath = fieldData.GetFullPath();

            Assert.Equal(testPath+"/"+name, fullPath);
        }

        [Fact]
        public void GetKeyUse_NullKey_ReturnsNull()
        {
            var keyUse = ChrysalisExtensions.GetKeyUse(null);
            Assert.Null(keyUse);
        }

        [Fact]
        public void GetKeyUse_KeyFieldsMissingInputData_ReturnsNull()
        {
            var field1 = new Mock<IField>().Object;
            var field2 = new Mock<IField>().Object;
            var subGroupMock = new Mock<ISubGroup>();
            subGroupMock.Setup(sg => sg.KeyFields).Returns(new List<IField> {field1, field2});

            var keyUse = subGroupMock.Object.GetKeyUse();

            Assert.Null(keyUse);
        }

        [Fact]
        public void GetKeyUse_SingleFieldNoPathElement_ReturnsFullPath()
        {
            var fullPath = "TestData";
            var mockIFieldData = new Mock<IFieldData>();
            mockIFieldData.Setup(f => f.Name).Returns(fullPath);
            mockIFieldData.Setup(f => f.ContentType).Returns(ContentType.Element);
            
            var fieldData = mockIFieldData.Object;

            var fieldMock = new Mock<IField>();
            fieldMock.SetupProperty(f => f.InputData);
            fieldMock.Object.InputData = fieldData;

            var subGroupMock = new Mock<ISubGroup>();
            subGroupMock.Setup(sg => sg.KeyFields).Returns(new List<IField> {fieldMock.Object});
            

            var keyUse = subGroupMock.Object.GetKeyUse();
            Assert.Equal(fullPath, keyUse);
        }

        [Fact]
        public void GetKeyUse_MultipleFieldNoPathElements_ReturnsFormattedString()
        {
            var fullPath1 = "TestData1";
            var fullPath2 = "TestData2";

            var mockIFieldData1 = new Mock<IFieldData>();
            mockIFieldData1.Setup(f => f.Name).Returns(fullPath1);
            mockIFieldData1.Setup(f => f.ContentType).Returns(ContentType.Element);

            var mockIFieldData2 = new Mock<IFieldData>();
            mockIFieldData2.Setup(f => f.Name).Returns(fullPath2);
            mockIFieldData2.Setup(f => f.ContentType).Returns(ContentType.Element);

            var mockField1 = new Mock<IField>();
            mockField1.SetupProperty(f => f.InputData);
            mockField1.Object.InputData = mockIFieldData1.Object;

            var mockField2 = new Mock<IField>();
            mockField2.SetupProperty(f => f.InputData);
            mockField2.Object.InputData = mockIFieldData2.Object;

            var mockSubGroup = new Mock<ISubGroup>();
            mockSubGroup.Setup(sg => sg.KeyFields).Returns(new List<IField> {mockField1.Object, mockField2.Object});

            var keyUse = mockSubGroup.Object.GetKeyUse();

            Assert.Matches(new Regex(@"^concat\(\w[\,\s\'\|\'\,\s\w]*\)$"), keyUse);
            Assert.Contains(fullPath1, keyUse);
            Assert.Contains(fullPath2, keyUse);
        }

        [Fact]
        public void GetContext_ValidInputContext_ReturnsInputContext()
        {
            var testContext = "TestContext";

            var mockSubGroup = new Mock<ISubGroup>();
            mockSubGroup.Setup(sg => sg.InputContext).Returns(testContext);


            var context = mockSubGroup.Object.GetContext();
            Assert.Equal(testContext, context);
        }

        [Fact]
        public void GetContext_NullGroup_ReturnsEmptyString()
        {
            var context = ChrysalisExtensions.GetContext(null);
            Assert.Equal(string.Empty, context);
        }

        [Fact]
        public void GetContext_NullInputContextValidOutputDataValidKey_ReturnsInputContext()
        {
            var outputDataName = "TestOutputData";
            var inputDataName = "TestInputData";

            var mockInputFieldData = new Mock<IFieldData>();
            mockInputFieldData.Setup(f => f.Name).Returns(inputDataName);

            var mockOutputFieldData = new Mock<IFieldData>();
            mockOutputFieldData.Setup(f => f.Name).Returns(outputDataName);

            var mockField = new Mock<IField>();
            mockField.SetupProperty(f => f.InputData);
            mockField.Object.InputData = mockInputFieldData.Object;

            var mockSubGroup = new Mock<ISubGroup>();
            mockSubGroup.Setup(sg => sg.KeyFields).Returns(new List<IField> { mockField.Object });
            mockSubGroup.SetupProperty(sg => sg.OutputData);
            mockSubGroup.Object.OutputData = mockOutputFieldData.Object;

            var context = mockSubGroup.Object.GetContext();
            Assert.Matches(new Regex(@"^key\(\'\w*\'\,\w*\)$"), context);
            Assert.Contains(outputDataName, context);
            Assert.Contains(inputDataName, context);
        }

        [Fact]
        public void GetContext_NullInputContextNullOutputDataValidKey_ReturnsEmptyString()
        {
            
            var inputDataName = "TestInputData";

            var mockFieldData = new Mock<IFieldData>();
            mockFieldData.Setup(f => f.Name).Returns(inputDataName);

            var mockField = new Mock<IField>();
            mockField.SetupProperty(f => f.InputData);
            mockField.Object.InputData = mockFieldData.Object;

            var mockSubGroup = new Mock<ISubGroup>();
            mockSubGroup.Setup(sg => sg.KeyFields).Returns(new List<IField> {mockField.Object});

            
            var context = mockSubGroup.Object.GetContext();
            Assert.Contains(string.Empty, context);
        }

        [Fact]
        public void GetContext_NullInputContextValidOutputInvalidKey_ReturnsEmptyString()
        {
            var outputDataName = "TestOutputData";

            var mockOutputFieldData = new Mock<IFieldData>();
            mockOutputFieldData.Setup(f => f.Name).Returns(outputDataName);

            var mockSubGroup = new Mock<ISubGroup>();
            mockSubGroup.SetupProperty(sg => sg.OutputData);
            mockSubGroup.Object.OutputData = mockOutputFieldData.Object;

            var context = mockSubGroup.Object.GetContext();
            Assert.Contains(string.Empty, context);
        }
    }
}