using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DataAccess.ObsoleteIFactory
{
    public interface ISlipDamageViewInfoFactory
    {
        public SlipDamageViewInfoValueObject Create(SlipDamageCode slipDamageCode);
    }
}