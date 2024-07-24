using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.IFactory
{
    public interface IAilmentFactory
    {
        public AilmentEntity Create(CharacterId characterId, AilmentCode ailmentCode);
    }
}