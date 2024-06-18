using System;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Id
{
    public abstract class AutoGenerationId : IId, IComparable<AutoGenerationId>
    {
        private readonly Guid _guid = Guid.NewGuid();

        public int CompareTo(AutoGenerationId other)
        {
            return GetHashCode() - other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var id = (AutoGenerationId)obj;
            return _guid == id._guid;
        }

        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }

        public override string ToString()
        {
            return _guid.ToString("N");
        }
    }
}