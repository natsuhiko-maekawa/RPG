using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Id
{
    public class BuffId : AbstractId<BuffId, HashId>
    {
        private readonly HashId _hashId;
        
        public BuffId(
            CharacterId characterId,
            BuffCode buffCode)
        {
            var tuple = (characterId, buffCode);
            _hashId = new HashId(tuple);
        }
    }
}