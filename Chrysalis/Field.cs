using System.Runtime.Serialization;

namespace Transmogrifier.Chrysalis
{
    [DataContract(Namespace = "https://www.transmogrifier.com/chrysalis")]
    internal class Field : IField
    {
        [DataMember(Name = "Alias")]
        private readonly string alias;

        [DataMember(EmitDefaultValue = false, Name = "InputData")]
        private FieldData inputData;

        [DataMember(EmitDefaultValue = false, Name = "OutputData")]
        private FieldData outputData;

        public Field(string alias, IFieldData inputData = null, IFieldData outputData = null)
        {
            this.alias = alias;
            this.InputData = inputData;
            this.OutputData = outputData;
        }

        public string Alias => alias;

        [DataMember]
        public string DataType { get; set; }

        
        public IFieldData InputData { get => inputData; set => inputData = (FieldData)value; }
        public IFieldData OutputData { get => outputData; set => outputData = (FieldData)value; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((Field) obj);
        }

        public override int GetHashCode() => alias != null ? alias.GetHashCode() : 0;

        protected bool Equals(Field other) => string.Equals(Alias, other.Alias);
    }
}