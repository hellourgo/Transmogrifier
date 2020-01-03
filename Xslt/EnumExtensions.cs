using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Transmogrifier.Xslt
{
    /// <summary>
    /// Extension methods for Enum types
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBeInternal")]
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description attribute from an Enum value
        /// </summary>
        /// <param name="value"></param>
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[]) fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes?.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}