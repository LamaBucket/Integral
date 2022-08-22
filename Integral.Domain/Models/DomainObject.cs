namespace Integral.Domain.Models
{
    public class DomainObject
    {
        public int Id { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj is DomainObject dObj)
            {
                return Id == dObj.Id;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(DomainObject? left, DomainObject? right)
        {
            return left?.Id == right?.Id;
        }

        public static bool operator !=(DomainObject? left, DomainObject? right)
        {
            return left?.Id != right?.Id;
        }
    }
}
