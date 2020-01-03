using System.Linq;
using Transmogrifier.Chrysalis;
using Xunit;

namespace Transmogrifier.ChrysalisTests
{
    public class SubGroupTests
    {
        [Fact]
        public void SubGroup_Default_SerializeTest()
        {
            var subGroup = new SubGroup("TestField");
            var element = SerializeUtilities.SerializeToXElement(subGroup);

            Assert.NotNull(element);
            Assert.Equal("SubGroup", element.Name.LocalName);
            Assert.Contains(element.Elements(), e => e.Name.LocalName == "Key");
        }

        [Fact]
        public void Key_NewSubGroup_DefaultsToNewKey()
        {
            var subGroup = new SubGroup("/");
            Assert.NotNull(subGroup.Key);
        }

        [Fact]
        public void KeyFields_Get_ReturnsParentAndChildKeys()
        {
            var parentKeyField = new Field("ParentKey");
            var childKeyField = new Field("ChildKeyField");
            var rootGroup = new SubGroup("/");
            rootGroup.AddKeyField(parentKeyField);
            var childGroup = new SubGroup("Child");
            childGroup.AddKeyField(childKeyField);
            rootGroup.AddGroup(childGroup);

            Assert.Contains(parentKeyField, childGroup.KeyFields);
            Assert.Contains(childKeyField, childGroup.KeyFields);
        }

        [Fact]
        public void AddKeyField_EmptyKeyFields_AddsFieldToKeyFields()
        {
            var testField = new Field("TestField");
            var subGroup = new SubGroup("/");
            subGroup.AddKeyField(testField);

            Assert.Contains(testField, subGroup.KeyFields);
        }

        [Fact]
        public void AddKeyField_Null_DoesNothing()
        {
            var subGroup = new SubGroup("/");
            subGroup.AddKeyField(null);

            Assert.Empty(subGroup.KeyFields);
        }

        [Fact]
        public void AddKeyField_EmptyKeyFieldsAndFields_AddsFieldToKeyFieldsAndFields()
        {
            var testField = new Field("TestField");
            var subGroup = new SubGroup("/");
            subGroup.AddKeyField(testField);

            Assert.Contains(testField, subGroup.KeyFields);
            Assert.Contains(testField, subGroup.Fields);
        }

        [Fact]
        public void AddKeyField_KeyFieldsContainsField_DoesNothing()
        {
            var testField = new Field("TestField");
            var subGroup = new SubGroup("/");
            subGroup.AddKeyField(testField);

            Assert.Contains(testField, subGroup.KeyFields);
            Assert.Equal(1, subGroup.KeyFields.Count());

            subGroup.AddKeyField(testField);
            subGroup.AddKeyField(testField);
            subGroup.AddKeyField(testField);

            Assert.Contains(testField, subGroup.KeyFields);
            Assert.Equal(1, subGroup.KeyFields.Count());
        }

        [Fact]
        public void RemoveKeyField_EmptyKeyFields_DoesNothing()
        {
            var testField = new Field("TestField");
            var subGroup = new SubGroup("/");
            
            subGroup.RemoveKeyField(testField);
            Assert.Empty(subGroup.KeyFields);
        }

        [Fact]
        public void RemoveKeyField_SubGroupOwnsKeyField_RemovesKeyField()
        {
            var testField = new Field("TestField");
            var subGroup = new SubGroup("/");
            subGroup.AddKeyField(testField);
            Assert.Contains(testField, subGroup.KeyFields);

            subGroup.RemoveKeyField(testField);
            Assert.Empty(subGroup.KeyFields);
        }

        [Fact]
        public void RemoveKeyField_ParentGroupOwnsKeyField_DoesNothing()
        {
            var parentKeyField = new Field("ParentKeyField");
            var childKeyField = new Field("SubKeyField");
            var parentGroup = new SubGroup("Parent");
            var childGroup = new SubGroup("Child");
            parentGroup.AddKeyField(parentKeyField);
            parentGroup.AddGroup(childGroup);
            childGroup.AddKeyField(childKeyField);

            Assert.Contains(parentKeyField, childGroup.KeyFields);
            Assert.Contains(childKeyField, childGroup.KeyFields);

            childGroup.RemoveKeyField(parentKeyField);
            Assert.Contains(parentKeyField, childGroup.KeyFields);
            Assert.Contains(childKeyField, childGroup.KeyFields);
        }

        [Fact]
        public void RemoveField_KeyFieldHasFieldAndGroupOwnsKeyField_RemovesKeyField()
        {
            var testField = new Field("TestField");
            var subGroup = new SubGroup("/");
            subGroup.AddKeyField(testField);
            Assert.Contains(testField, subGroup.KeyFields);
            Assert.Contains(testField, subGroup.Fields);
            subGroup.RemoveField(testField);

            Assert.DoesNotContain(testField, subGroup.KeyFields);
            Assert.DoesNotContain(testField, subGroup.Fields);
        }
    }
}