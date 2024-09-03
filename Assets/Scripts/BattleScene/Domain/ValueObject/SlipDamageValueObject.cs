using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class SlipDamageValueObject
    {
        public SlipDamageValueObject(
            SlipDamageCode slipDamageCode,
            ImmutableList<AttackValueObject> damageList)
        {
            SlipDamageCode = slipDamageCode;
            AttackList = damageList;
        }

        public SlipDamageCode SlipDamageCode { get; }
        public ImmutableList<AttackValueObject> AttackList { get; }

        public int GetTotal()
        {
            return AttackList
                .Sum(x => x.Amount);
        }
    }
}