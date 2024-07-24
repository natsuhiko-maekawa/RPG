using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.OldId
{
    public class BuffId : AbstractId<BuffId, HashId>
    {
        protected override HashId Id { get; }

        public BuffId(
            CharacterId characterId,
            BuffCode buffCode)
        {
            var tuple = (characterId, buffCode);
            Id = new HashId(tuple);
        }
    }
}