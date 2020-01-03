using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Transmogrifier.Xslt
{
    /// <summary>
    /// Extension methods for working with <see cref="XsltElementType"/> enums.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBeInternal")]
    public static class XsltElementTypeExtensions
    {
        /// <summary>
        /// Parses the <see cref="XsltElementType"/> from a string.
        /// </summary>
        /// <param name="description">The description.</param>
        public static XsltElementType ParseXsltElementType(this string description)
        {
            return Enum.GetValues(typeof(XsltElementType))
                       .Cast<XsltElementType>()
                       .FirstOrDefault(e => e.GetDescription() == description);
        }
    }

    /// <summary>
    /// XSLT 1.0 element types.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum XsltElementType
    {
        None,
        /// <summary>
        /// <c>stylesheet</c>
        /// </summary>
        [Description("stylesheet")] Stylesheet,
        /// <summary>
        /// <c>attribute-set</c>
        /// </summary>
        [Description("attribute-set")] AttributeSet,
        /// <summary>
        /// <c>decimal-format</c>
        /// </summary>
        [Description("decimal-format")] DecimalFormat,

        /// <summary>
        /// <c>import</c>
        /// </summary>
        [Description("import")] Import,
        /// <summary>
        /// <c>include</c>
        /// </summary>
        [Description("include")] Include,
        /// <summary>
        /// <c>key</c>
        /// </summary>
        [Description("key")] Key,
        /// <summary>
        /// <c>namespace-alias</c>
        /// </summary>
        [Description("namespace-alias")] NamespaceAlias,
        /// <summary>
        /// <c>output</c>
        /// </summary>
        [Description("output")] Output,
        /// <summary>
        /// <c>param</c>
        /// </summary>
        [Description("param")] Param,
        /// <summary>
        /// <c>preserve-space</c>
        /// </summary>
        [Description("preserve-space")] PreserveSpace,
        /// <summary>
        /// <c>strip-space</c>
        /// </summary>
        [Description("strip-space")] StripSpace,
        /// <summary>
        /// <c>template</c>
        /// </summary>
        [Description("template")] Template,
        /// <summary>
        /// <c>variable</c>
        /// </summary>
        [Description("variable")] Variable,
        /// <summary>
        /// <c>apply-imports</c>
        /// </summary>
        [Description("apply-imports")] ApplyImports,
        /// <summary>
        /// <c>apply-templates</c>
        /// </summary>
        [Description("apply-templates")] ApplyTemplates,
        /// <summary>
        /// <c>attribute</c>
        /// </summary>
        [Description("attribute")] Attribute,
        /// <summary>
        /// <c>call-template</c>
        /// </summary>
        [Description("call-template")] CallTemplate,
        /// <summary>
        /// <c>choose</c>
        /// </summary>
        [Description("choose")] Choose,
        /// <summary>
        /// <c>comment</c>
        /// </summary>
        [Description("comment")] Comment,
        /// <summary>
        /// <c>copy</c>
        /// </summary>
        [Description("copy")] Copy,
        /// <summary>
        /// <c>copyof</c>
        /// </summary>
        [Description("copyof")] CopyOf,
        /// <summary>
        /// <c>element</c>
        /// </summary>
        [Description("element")] Element,
        /// <summary>
        /// <c>fallback</c>
        /// </summary>
        [Description("fallback")] Fallback,
        /// <summary>
        /// <c>for-each</c>
        /// </summary>
        [Description("for-each")] ForEach,
        /// <summary>
        /// <c>if</c>
        /// </summary>
        [Description("if")] If,
        /// <summary>
        /// <c>message</c>
        /// </summary>
        [Description("message")] Message,
        /// <summary>
        /// <c>number</c>
        /// </summary>
        [Description("number")] Number,
        /// <summary>
        /// <c>otherwise</c>
        /// </summary>
        [Description("otherwise")] Otherwise,

        /// <summary>
        /// <c>processing-instruction</c>
        /// </summary>
        [Description("processing-instruction")]
        ProcessingInstruction,
        /// <summary>
        /// <c>sort</c>
        /// </summary>
        [Description("sort")] Sort,
        /// <summary>
        /// <c>text</c>
        /// </summary>
        [Description("text")] Text,
        /// <summary>
        /// <c>value-of</c>
        /// </summary>
        [Description("value-of")] ValueOf,
        /// <summary>
        /// <c>with-paranm</c>
        /// </summary>
        [Description("with-param")] WithParam,
        /// <summary>
        /// <c>when</c>
        /// </summary>
        [Description("when")] When
    }
}