using BattleScene.Domain.Interface;

namespace BattleScene.Domain.OldId
{
    public class HashId : AbstractId<HashId, int>
    {
        protected override int Id { get; }

        public HashId(object obj)
        {
            Id = obj.GetHashCode();
        }
    }
}