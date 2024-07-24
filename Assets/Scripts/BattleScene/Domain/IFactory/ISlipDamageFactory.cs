using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.IFactory
{
    public interface ISlipDamageFactory
    {
        public SlipDamageEntity Create(CharacterId playerId, CharacterId enemyId, SlipDamageCode slipDamageCode);
    }
}