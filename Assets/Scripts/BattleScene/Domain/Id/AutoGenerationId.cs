using System;

namespace BattleScene.Domain.Id
{
    public abstract class AutoGenerationId
    {
        private readonly Guid _guid = Guid.NewGuid();

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