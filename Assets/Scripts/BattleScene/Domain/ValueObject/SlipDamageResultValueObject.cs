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
            ImmutableList<DamageValueObject> damageList)
        {
            SlipDamageCode = slipDamageCode;
            DamageList = damageList;
        }

        public SlipDamageCode SlipDamageCode { get; }
        public ImmutableList<DamageValueObject> DamageList { get; }

        public int GetTotal()
        {
            return DamageList
                .Sum(x => x.Amount);
        }
    }
}