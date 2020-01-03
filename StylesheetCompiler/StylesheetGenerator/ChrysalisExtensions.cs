using System.Linq;
using Transmogrifier.Chrysalis;

namespace Transmogrifier.StylesheetCompiler.StylesheetGenerator
{
    internal static class ChrysalisExtensions
    {
        public static string GetContext(this IGroup group)
        {
            if (!string.IsNullOrEmpty(group?.InputContext)) return group.InputContext;

            return group is ISubGroup subGroup && subGroup.OutputData?.Name != null && subGroup.GetKeyUse() != null
                ? $"key('{subGroup.OutputData.Name}',{subGroup.GetKeyUse()})"
                : string.Empty;
        }

        public static string GetFullPath(this IFieldData fieldData)
        {
            if (fieldData?.Name == null) return string.Empty;

            var inputName = fieldData.Name;
            if (fieldData.ContentType == ContentType.Attribute)
                inputName = $"@{inputName}";

            return fieldData.HasPath() ? $"{fieldData.Path}/{inputName}" : inputName;
        }

        public static string GetKeyUse(this ISubGroup group)
        {
            if (group == null || group.KeyFields.All(f => f.InputData == null)) return null;

            var inputDatas = group.KeyFields.Where(f => f.InputData != null).Select(f => f.InputData.GetFullPath())
                                .ToList();

            var pathString = inputDatas
                .Aggregate((output, path) => string.IsNullOrEmpty(output) ? path : output + $", '|', {path}");

            return inputDatas.Count > 1 ? $"concat({pathString})" : pathString;
        }
    }
}