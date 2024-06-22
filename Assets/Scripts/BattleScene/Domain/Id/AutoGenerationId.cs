using System;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Id
{
    public abstract class AutoGenerationId : AbstractId<AutoGenerationId, Guid>, IComparable<AutoGenerationId>
    {
        private readonly Guid _guid = Guid.NewGuid();

        public int CompareTo(AutoGenerationId other)
        {
            return GetHashCode() - other.GetHashCode();
        }

        public override string ToString()
        {
            return _guid.ToString("N");
        }
    }
}