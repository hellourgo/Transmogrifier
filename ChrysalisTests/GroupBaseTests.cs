using System.Linq;
using System.Runtime.Serialization;
using Transmogrifier.Chrysalis;
using Xunit;

namespace Transmogrifier.ChrysalisTests
{
    public class GroupBaseTests
    {
        [DataContract(Name = "TestGroup")]
        // ReSharper disable once InconsistentNaming
        private sealed class TestGroup_InheritsGroupBase : GroupBase
        {
            public TestGroup_InheritsGroupBase()
            {
            }

            public TestGroup_InheritsGroupBase(string templateMatch) : base(templateMatch)
            {
            }
        }

        [Fact]
        [Trait("Category","Serialization")]
        public void GroupBase_Default_Serializes()
        {
            var group = new TestGroup_InheritsGroupBase("TestGroup");
            var element = SerializeUtilities.SerializeToXElement(group);

            Assert.NotNull(element);
            Assert.Equal("TestGroup", element.Name.LocalName);
        }

        [Theory]
        [Trait("Category", "Serialization")]
        [InlineData("TemplateMatch")]
        [InlineData("Fields")]
        [InlineData("OutputData")]
        [InlineData("Groups")]
        public void GroupBase_Default_SerializesRequiredElements(string expectedElement)
        {
            var group = new TestGroup_InheritsGroupBase("TestGroup");
            var element = SerializeUtilities.SerializeToXElement(group);

            Assert.Contains(element.Elements(), e => e.Name.LocalName == expectedElement);
        }

        [Fact]
        [Trait("Category", "Serialization")]
        public void GroupBase_Default_DoesNotSerializeOptionalElements()
        {
            var group = new TestGroup_InheritsGroupBase("TestGroup");
            var element = SerializeUtilities.SerializeToXElement(group);

            Assert.DoesNotContain(element.Elements(), e => e.Name.LocalName == "InputContext");
        }

        [Fact]
        [Trait("Category", "Serialization")]
        public void GroupBase_InputContextPopulated_Serializes()
        {
            var group = new TestGroup_InheritsGroupBase("TestGroup");
            group.InputContext = "Test";
            var element = SerializeUtilities.SerializeToXElement(group);

            Assert.Contains(element.Elements(), e => e.Name.LocalName == "InputContext");
        }

        [Fact]
        public void AddField_GroupDoesNotContainField_AddsFieldToGroup()
        {
            var group = new TestGroup_InheritsGroupBase();
            var field = new Field("TestField");

            Assert.Empty(group.Fields);
            group.AddField(field);
            Assert.Contains(field, group.Fields);
        }

        [Fact]
        public void AddFields_GroupDoesNotContainField_AddsFieldsToGroup()
        {
            var group = new TestGroup_InheritsGroupBase();
            var field1 = new Field("TestField");
            var field2 = new Field("TestField");

            Assert.Empty(group.Fields);
            group.AddFields(field1, field2);

            Assert.Contains(field1, group.Fields);
            Assert.Contains(field2, group.Fields);
        }

        [Fact]
        public void AddField_GroupContainsField_DoesNothing()
        {
            var testGroupBase = new TestGroup_InheritsGroupBase();
            var field = new Field("TestField");

            testGroupBase.AddField(field);
            Assert.Contains(field, testGroupBase.Fields);
            Assert.Equal(1, testGroupBase.Fields.Count());

            testGroupBase.AddField(field);
            Assert.Contains(field, testGroupBase.Fields);
            Assert.Equal(1, testGroupBase.Fields.Count());
        }

        [Fact]
        public void AddField_Null_DoesNothing()
        {
            var testGroupBase = new TestGroup_InheritsGroupBase();

            testGroupBase.AddField(null);
            Assert.Empty(testGroupBase.Fields);
        }

        [Fact]
        public void AddFields_Null_DoesNothing()
        {
            var testGroupBase = new TestGroup_InheritsGroupBase();

            testGroupBase.AddFields(null);
            Assert.Empty(testGroupBase.Fields);
        }

        [Fact]
        public void RemoveField_GroupContainsField_FieldRemovedFromGroup()
        {
            var testGroupBase = new TestGroup_InheritsGroupBase();
            var field = new Field("TestField");

            testGroupBase.AddField(field);
            Assert.Contains(field, testGroupBase.Fields);
            Assert.Equal(1, testGroupBase.Fields.Count());

            testGroupBase.RemoveField(field);
            Assert.DoesNotContain(field, testGroupBase.Fields);
            Assert.Empty(testGroupBase.Fields);
        }

        [Fact]
        public void RemoveField_GroupDoesNotContainField_DoesNothing()
        {
            var testGroupBase = new TestGroup_InheritsGroupBase();
            var addedField = new Field("TestField");
            var otherField = new Field("OtherField");

            testGroupBase.AddField(addedField);
            Assert.Contains(addedField, testGroupBase.Fields);
            Assert.Equal(1, testGroupBase.Fields.Count());

            testGroupBase.RemoveField(otherField);
            Assert.DoesNotContain(otherField, testGroupBase.Fields);
            Assert.Contains(addedField, testGroupBase.Fields);
            Assert.Equal(1, testGroupBase.Fields.Count());
        }

        [Fact]
        public void AddGroup_SubGroupDoesNotHaveParent_AddsGroupToChildren()
        {
            var parent = new TestGroup_InheritsGroupBase();
            var subGroup = new SubGroup("Child");

            parent.AddGroup(subGroup);
            Assert.Contains(subGroup, parent.SubGroups);
        }

        [Fact]
        public void AddGroup_SubGroupDoesNotHaveParent_SetsParentProperty()
        {
            var parent = new TestGroup_InheritsGroupBase();
            var subGroup = new SubGroup("Child");

            parent.AddGroup(subGroup);
            Assert.Equal(parent, subGroup.Parent);
        }

        [Fact]
        public void AddGroup_SubGroupHasParent_OriginalParentRemovesChild()
        {
            var originalParent = new TestGroup_InheritsGroupBase("OriginalParent");
            var subGroup = new SubGroup("Child");
            originalParent.AddGroup(subGroup);

            var newParent = new TestGroup_InheritsGroupBase("NewParent");
            newParent.AddGroup(subGroup);

            Assert.DoesNotContain(subGroup, originalParent.SubGroups);
            Assert.Same(newParent, subGroup.Parent);
        }

        [Fact]
        public void RemoveGroup_Group_RemovesChildFromGroups()
        {
            var rootGroup = new TestGroup_InheritsGroupBase("/");
            var subGroup = new SubGroup("Child");

            rootGroup.AddGroup(subGroup);
            rootGroup.RemoveGroup(subGroup);

            Assert.DoesNotContain(subGroup, rootGroup.SubGroups);
        }

        [Fact]
        public void RemoveGroup_ParentDoesNotContainChild_DoesNothing()
        {
            var rootGroup = new TestGroup_InheritsGroupBase("/");
            var subGroup = new SubGroup("Child");
            var otherSubGroup = new SubGroup("Other");
            rootGroup.AddGroup(subGroup);
            rootGroup.RemoveGroup(otherSubGroup);

            Assert.Contains(subGroup, rootGroup.SubGroups);
            Assert.DoesNotContain(otherSubGroup, rootGroup.SubGroups);
        }

        [Fact]
        public void RemoveGroup_Group_RemovesParentFromChild()
        {
            var rootGroup = new TestGroup_InheritsGroupBase("/");
            var childGroup = new SubGroup("Child");

            rootGroup.AddGroup(childGroup);
            rootGroup.RemoveGroup(childGroup);

            Assert.Null(childGroup.Parent);
        }
    }
}