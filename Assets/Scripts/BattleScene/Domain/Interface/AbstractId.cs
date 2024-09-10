namespace BattleScene.Domain.Interface
{
    public abstract class AbstractId<TId, T> where TId : AbstractId<TId, T>
    {
        protected abstract T Id { get; }
        
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