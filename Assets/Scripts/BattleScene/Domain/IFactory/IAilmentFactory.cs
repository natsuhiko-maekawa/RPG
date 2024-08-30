using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IFactory
{
    public interface IAilmentFactory
    {
        public AilmentEntity Create(CharacterId characterId, AilmentCode ailmentCode);
    }
}