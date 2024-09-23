using System.Collections.Generic;
using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.PrimeSkill.BaseClass
{
    public class BaseReset
    {
        public virtual IReadOnlyList<AilmentCode> AilmentCodeList { get; }
        public virtual IReadOnlyList<SlipDamageCode> SlipDamageCodeList { get; }
        public virtual IReadOnlyList<BodyPartCode> BodyPartCodeList { get; }
    }
}