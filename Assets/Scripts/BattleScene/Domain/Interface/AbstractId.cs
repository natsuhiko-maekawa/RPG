namespace BattleScene.Domain.Interface
{
    public abstract class AbstractId<TId, T> : IId where TId : AbstractId<TId, T>
    {
        private readonly T _id;
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var id = (TId)obj;
            return Equals(_id, id._id);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}