using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.IRepository
{
    public interface ISlipDamageRepository
    {
        public ImmutableList<SlipDamageEntity> Select();
        public SlipDamageEntity Select(SlipDamageCode slipDamageCode);
    }
}