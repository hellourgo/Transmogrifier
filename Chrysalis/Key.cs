using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Transmogrifier.Chrysalis
{
    [DataContract(Namespace = "https://www.transmogrifier.com/chrysalis")]
    internal class Key
    {
        [DataMember(Name = "KeyFields")] private List<Field> keyFields = new List<Field>();

        public Key()
        {
        }

        public Key(params Field[] fields)
        {
            if (fields != null)
                KeyFields.AddRange(fields.Where(f => f != null));
        }

        public List<Field> KeyFields => keyFields;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((Key) obj);
        }

        protected bool Equals(Key other) =>
            KeyFields.All(other.KeyFields.Contains) && KeyFields.Count == other.KeyFields.Count;
    }
}