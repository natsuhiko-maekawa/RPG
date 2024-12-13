using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public record RecoveryValueObject(
        IReadOnlyList<AilmentCode>? AilmentCodeList = null,
        IReadOnlyList<SlipCode>? SlipCodeList = null,
        IReadOnlyList<BodyPartCode>? BodyPartCodeList = null)
    {
        public IReadOnlyList<AilmentCode> AilmentCodeList { get; private set; } = 
            AilmentCodeList ?? Array.Empty<AilmentCode>();
        public IReadOnlyList<SlipCode> SlipCodeList { get; private set; } = 
            SlipCodeList ?? Array.Empty<SlipCode>();
        public IReadOnlyList<BodyPartCode> BodyPartCodeList { get; private set; } = 
            BodyPartCodeList ?? Array.Empty<BodyPartCode>();
    }
}