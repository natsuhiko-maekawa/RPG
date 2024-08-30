using System;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Id
{
    public abstract class AutoGenerationId : AbstractId<AutoGenerationId, Guid>, IComparable<AutoGenerationId>
    {
        protected override Guid Id { get; } = Guid.NewGuid();

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