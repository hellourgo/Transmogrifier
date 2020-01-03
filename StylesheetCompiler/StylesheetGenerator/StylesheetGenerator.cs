using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Transmogrifier.Chrysalis;
using Transmogrifier.StylesheetCompiler.TransformBuilder;
using Transmogrifier.Xslt;

namespace Transmogrifier.StylesheetCompiler.StylesheetGenerator
{
    public class StylesheetGenerator
    {
        private readonly XsltFactory xslt;

        public StylesheetGenerator(XsltFactory xslt = null)
        {
            this.xslt = xslt ?? new XsltFactory();
        }

        public XDocument CreateStylesheet(IRootGroup rootGroup)
        {
            var configuration = new TransformBuilderConfiguration
            {
                OutputType = rootGroup.OutputType,
                FormatXml = true,
                IgnoreWhitespace = true
            };
            var builder = new TransformBuilder.TransformBuilder(configuration);

            CreateKeyElements(builder, rootGroup);
            CreateTemplateElements(builder, rootGroup);

            return builder.Build();
        }

        public List<XDocument> CreateStylesheets(IChrysalis chrysalis) =>
            chrysalis.RootGroups.Select(CreateStylesheet).ToList();

        public XsltElement GetKeyElement(IGroup group)
        {
            if (!(group is ISubGroup subgroup)) return null;
            return  !subgroup.KeyFields.Any()
                ? null
                : xslt.Key(subgroup.OutputData.Name, subgroup.TemplateMatch, subgroup.GetKeyUse());
            
        }

        private string GetTemplateElementMode(IGroup group)
        {
            if (!(group is ISubGroup subGroup)) return null;
                return subGroup.KeyFields.Any() ? group.OutputData.Name : null;
        }

        public XsltElement GetTemplateElement(IGroup group)
        {
            var templateElement =
                xslt.Template(group.TemplateMatch, GetTemplateElementMode(group));

            XsltElement groupElement;

            if (group is IRootGroup rootGroup)
                groupElement = templateElement.GetPathEndpoint(GetPathPartElements(rootGroup.OutputData))
                                              .AddAndReturnChild(xslt.Element(rootGroup.OutputData.Name));
            else
                groupElement = templateElement.AddAndReturnChild(xslt.Element(group.OutputData.Name));

            foreach (var field in group.Fields.Where(f => f.OutputData != null).OrderBy(f => f.OutputData.ContentType))
            {
                groupElement.GetPathEndpoint(GetPathPartElements(field.OutputData))
                            .AddOrUpdateOutputElements(GetOutputTransformElement(field));
            }

            foreach (var subgroup in group.SubGroups)
            {
                groupElement.GetPathEndpoint(GetPathPartElements(subgroup.OutputData))
                            .AddOrUpdateOutputElements(GetApplyTemplatesElement(group.GetContext(), subgroup));
            }

            return templateElement;
        }

        private IEnumerable<XsltElement> GetPathPartElements(IFieldData fieldData)
        {
            var path = fieldData?.Path;
            if (string.IsNullOrEmpty(path)) return new List<XsltElement>();

            return path.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries).Select(p => xslt.Element(p));
        }

        private static string GetMuenchianGrouping(string context, string keyName, string keyUse) =>
            $"{context}[generate-id()  = generate-id(key('{keyName}',{keyUse})[1])]";

        internal void CreateKeyElements(TransformBuilder.TransformBuilder builder, IGroup parentGroup)
        {
            builder.Append(GetKeyElement(parentGroup));
            foreach (var childGroup in parentGroup.SubGroups)
            {
                CreateKeyElements(builder, childGroup);
            }
        }

        private void CreateTemplateElements(TransformBuilder.TransformBuilder builder, IGroup parentGroup)
        {
            builder.Append(GetTemplateElement(parentGroup));
            foreach (var childGroup in parentGroup.SubGroups)
            {
                CreateTemplateElements(builder, childGroup);
            }
        }

        private XsltElement GetApplyTemplatesElement(string context, IGroup childGroup)
        {
            if (!(childGroup is ISubGroup subGroup)) return null;
            var mode = subGroup.OutputData.Name;
            var select = GetMuenchianGrouping(context, mode, subGroup.GetKeyUse());
            return xslt.ApplyTemplates(select, mode);
        }

        internal XsltElement GetOutputElement(IFieldData fieldData)
        {
            switch (fieldData?.ContentType)
            {
                case ContentType.Variable:
                    return xslt.Variable(fieldData.Name);
                case ContentType.Attribute:
                    return xslt.Attribute(fieldData.Name);
                case ContentType.Element:
                    return xslt.Element(fieldData.Name);
                case ContentType.Text:
                    return xslt.Text();
                case ContentType.Number:
                    return xslt.Number();
                case ContentType.Calculation:
                case ContentType.Aggregation:
                    return xslt.ValueOf(fieldData.Name);
                default:
                    return null;
            }
        }

        private XsltElement GetOutputTransformElement(IField field)
        {
            var outputElement = GetOutputElement(field.OutputData);
            var valueElement = GetValueElement(field.InputData);

            outputElement?.Add(valueElement);

            return outputElement ?? valueElement;
        }

        private XsltElement GetValueElement(IFieldData fieldData)
        {
            if (fieldData == null) return null;
            switch (fieldData.ContentType)
            {
                case ContentType.Element:
                case ContentType.Attribute:
                    return xslt.ValueOf(fieldData.GetFullPath());
                case ContentType.Text:
                case ContentType.Number:
                    return xslt.Text(fieldData.Name);
                default:
                    return xslt.ValueOf(fieldData.Name);
            }
        }   
    }
}