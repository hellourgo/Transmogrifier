using Transmogrifier.Chrysalis;

namespace Transmogrifier.ChrysalisTests
{
    public class MockChrysalisFactory
    {
        public static IChrysalis CreateChrysalis(IChrysalisFactory factory = null)
        {
            if (factory == null) factory = new ChrysalisFactory();
            var chrysalis = factory.CreateChrysalis();

            var grandTotal = factory.CreateField("GrandTotal", factory.CreateFieldData("grandTotal"),
                factory.CreateFieldData("GrandTotal", ContentType.Element));

            var poNumber = new Field("PurchaseOrderNumber")
            {
                DataType = "string",
                InputData = new FieldData("PurchaseOrderNumber"),
                OutputData = new FieldData("PurchaseOrderNumber")
                {
                    Path = "Header",
                    ContentType = ContentType.Element
                }
            };
            var poTotal = new Field("LineTotal")
            {
                DataType = "decimal",
                InputData = new FieldData("poTotal"),
                OutputData = new FieldData("LineTotal")
                {
                    Path = "Header",
                    ContentType = ContentType.Element
                }
            };
            var lineNumber = new Field("LineNumber")
            {
                DataType = "string",
                InputData = new FieldData("line")
                {
                    ContentType = ContentType.Attribute
                },
                OutputData = new FieldData("number")
                {
                    ContentType = ContentType.Attribute
                }
            };
            var quantity = new Field("Quantity")
            {
                DataType = "decimal",
                InputData = new FieldData("Quantity"),
                OutputData = new FieldData("Quantity")
                {
                    ContentType = ContentType.Element
                }
            };
            var unitPrice = new Field("UnitPrice")
            {
                DataType = "decimal",
                InputData = new FieldData("UnitPrice"),
                OutputData = new FieldData("Price")
                {
                    ContentType = ContentType.Element
                }
            };
            var description = new Field("Description")
            {
                DataType = "string",
                InputData = new FieldData("Description"),
                OutputData = new FieldData("Description")
                {
                    ContentType = ContentType.Element
                }
            };
            var businessUnit = new Field("BusinessUnit")
            {
                DataType = "string",
                InputData = new FieldData("BusinessUnit"),
                OutputData = new FieldData("BusinessUnit")
                {
                    Path = "Header/Properties",
                    ContentType = ContentType.Element
                }
            };
            var currency = new Field("Currency")
            {
                DataType = "string",
                InputData = new FieldData("currency")
                {
                    Path = "UnitPrice",
                    ContentType = ContentType.Attribute
                },
                OutputData = new FieldData("curr")
                {
                    Path = "Price",
                    ContentType = ContentType.Attribute
                }
            };
            var taxIncluded = new Field("TaxIncluded")
            {
                DataType = "string",
                InputData = new FieldData("taxIncluded")
                {
                    Path = "UnitPrice",
                    ContentType = ContentType.Attribute
                },
                OutputData = new FieldData("taxIncluded")
                {
                    Path = "Price",
                    ContentType = ContentType.Attribute
                }
            };
            var constantField = new Field("ElTesto")
            {
                DataType = "string",
                InputData = new FieldData("This is a test one two three.")
                {
                    ContentType = ContentType.Text
                },
                OutputData = new FieldData("ElTesto")
                {
                    ContentType = ContentType.Element
                }
            };

            var calcSubTotal = new Field("SubTotal")
            {
                DataType = "decimal",
                InputData = new FieldData("Quantity*@line")
                {
                    ContentType = ContentType.Calculation
                },
                OutputData = new FieldData("SubTotal")
                {
                    ContentType = ContentType.Element
                }
            };
            var calcTaxTotal = new Field("TaxTotal")
            {
                DataType = "decimal",
                InputData = new FieldData("Quantity*@line*$TaxRate")
                {
                    ContentType = ContentType.Calculation
                },
                OutputData = new FieldData("TaxTotal")
                {
                    ContentType = ContentType.Element
                }
            };
            var variableField = new Field("TaxRate")
            {
                DataType = "decimal",
                InputData = new FieldData(".0825")
                {
                    ContentType = ContentType.Number
                },
                OutputData = new FieldData("TaxRate")
                {
                    ContentType = ContentType.Variable
                }
            };

            var lineGroup = factory.CreateGroup<ISubGroup>("Record");
            lineGroup.OutputData = new FieldData("Line");
            lineGroup.AddKeyField(lineNumber);
            lineGroup.AddFields(
                lineNumber,
                quantity,
                unitPrice,
                calcSubTotal,
                description,
                currency,
                calcTaxTotal,
                variableField,
                taxIncluded
            );

            var poGroup = factory.CreateGroup<ISubGroup>("Record");
            poGroup.OutputData = factory.CreateFieldData("PurchaseOrder");

            poGroup.AddKeyField(poNumber);
            poGroup.AddKeyField(businessUnit);

            poGroup.AddFields(poNumber, poTotal, businessUnit, constantField);
            poGroup.AddGroup(lineGroup);

            var rootGroup = factory.CreateGroup<IRootGroup>("/");
            rootGroup.InputContext = "/File/Record";
            rootGroup.OutputData = new FieldData("MasterData")
            {
                Path = "Transmogrifier"
            };

            rootGroup.AddField(grandTotal);
            rootGroup.AddGroup(poGroup);

            chrysalis.AddRootGroup(rootGroup);

            return chrysalis;
        }
    }
}