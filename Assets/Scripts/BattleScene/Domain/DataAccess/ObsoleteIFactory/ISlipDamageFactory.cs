using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.DataAccess.ObsoleteIFactory
{
    public interface ISlipDamageFactory
    {
        public SlipDamageEntity Create(CharacterId playerId, CharacterId enemyId, SlipDamageCode slipDamageCode);
    }
}