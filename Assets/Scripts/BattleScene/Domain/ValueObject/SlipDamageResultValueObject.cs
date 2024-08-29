using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class SlipDamageResultValueObject : IDamageResult
    {
        public SlipDamageResultValueObject(
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