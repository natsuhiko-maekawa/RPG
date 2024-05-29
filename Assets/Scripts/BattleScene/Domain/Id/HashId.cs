namespace BattleScene.Domain.Id
{
    public class HashId
    {
        private readonly int _id;

        public HashId(object obj)
        {
            _id = obj.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var id = (HashId)obj;
            return _id == id._id;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}