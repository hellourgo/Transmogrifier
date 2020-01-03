namespace Transmogrifier.Chrysalis
{
    public abstract partial class GroupBase
    {
        protected bool Equals(GroupBase other) => string.Equals(TemplateMatch, other.TemplateMatch) && Equals(OutputData, other.OutputData);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((GroupBase) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((TemplateMatch != null ? TemplateMatch.GetHashCode() : 0) * 397) ^ (OutputData != null ? OutputData.GetHashCode() : 0);
            }
        }
    }
}