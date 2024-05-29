using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class OrderedSlipDamageValueObject : IOrderedItem
    {
        public SlipDamageCode SlipDamageCode { get; }

        public OrderedSlipDamageValueObject(
            SlipDamageCode slipDamageCode)
        {
            SlipDamageCode = slipDamageCode;
        }
    }
}