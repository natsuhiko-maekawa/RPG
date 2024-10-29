using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.PrimeSkill.BaseClass
{
    public abstract class BaseReset
    {
        public virtual IReadOnlyList<AilmentCode> AilmentCodeList { get; } = Array.Empty<AilmentCode>();
        public virtual IReadOnlyList<SlipCode> SlipCodeList { get; } = Array.Empty<SlipCode>();
        public virtual IReadOnlyList<BodyPartCode> BodyPartCodeList { get; } = Array.Empty<BodyPartCode>();
    }
}