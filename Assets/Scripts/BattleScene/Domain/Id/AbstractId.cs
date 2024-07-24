using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Id
{
    public abstract class AbstractId<TId, T> : IId where TId : AbstractId<TId, T>
    {
        public abstract T Id { get; }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var idObject = (TId)obj;
            return Equals(Id, idObject.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}