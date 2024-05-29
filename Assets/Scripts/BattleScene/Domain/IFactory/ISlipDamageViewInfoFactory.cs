using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.IFactory
{
    public interface ISlipDamageViewInfoFactory
    {
        public SlipDamageViewInfoValueObject Create(SlipDamageCode slipDamageCode);
    }
}