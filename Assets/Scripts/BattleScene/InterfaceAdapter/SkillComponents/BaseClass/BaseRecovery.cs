using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;

namespace BattleScene.InterfaceAdapter.SkillComponents.BaseClass
{
    public abstract class BaseRecovery
    {
        public virtual IReadOnlyList<AilmentCode> AilmentCodeList { get; } = Array.Empty<AilmentCode>();
        public virtual IReadOnlyList<SlipCode> SlipCodeList { get; } = Array.Empty<SlipCode>();
        public virtual IReadOnlyList<BodyPartCode> BodyPartCodeList { get; } = Array.Empty<BodyPartCode>();
    }
}