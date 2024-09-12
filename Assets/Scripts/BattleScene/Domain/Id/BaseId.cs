using System;

namespace BattleScene.Domain.Id
{
    public abstract class BaseId : IComparable<BaseId>
    {
        public string Id { get; } = Guid.NewGuid().ToString("N");

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var idObject = (BaseId)obj;
            return Equals(Id, idObject.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        
        public int CompareTo(BaseId other)
        {
            return GetHashCode() - other.GetHashCode();
        }

        public override string ToString()
        {
            return Id;
        }
    }
}