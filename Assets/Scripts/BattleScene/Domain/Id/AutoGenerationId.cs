using System;

namespace BattleScene.Domain.Id
{
    public abstract class AutoGenerationId : IComparable<AutoGenerationId>
    {
        private Guid Id { get; } = Guid.NewGuid();

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var idObject = (AutoGenerationId)obj;
            return Equals(Id, idObject.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        
        public int CompareTo(AutoGenerationId other)
        {
            return GetHashCode() - other.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString("N");
        }
    }
}