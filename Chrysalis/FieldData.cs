using System.Runtime.Serialization;

namespace Transmogrifier.Chrysalis
{
    [DataContract(Namespace = "https://www.transmogrifier.com/chrysalis")]
    internal class FieldData : IFieldData
    {
        [DataMember(Name = "Name")]
        private readonly string name;
        
        public string Name => name;
        [DataMember (EmitDefaultValue = false)]
        public string Path { get; set; }
        [DataMember]
        public ContentType ContentType { get; set; }

        public FieldData(string name, ContentType contentType = ContentType.None, string path = null)
        {
            this.name = name;
            ContentType = contentType;
            Path = path;
        }

        public bool HasPath() => !string.IsNullOrEmpty(Path);

        protected bool Equals(FieldData other) => string.Equals(Name, other.Name) && string.Equals(Path, other.Path) && ContentType == other.ContentType;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((FieldData) obj);
        }
    }
}