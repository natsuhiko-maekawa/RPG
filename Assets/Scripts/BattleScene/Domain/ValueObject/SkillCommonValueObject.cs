﻿using System.Collections.Generic;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record SkillCommonValueObject(
        SkillCode SkillCode,
        int TechnicalPoint,
        IReadOnlyList<BodyPartCode> DependencyList,
        Range Range,
        bool IsAutoTarget,
        MessageCode AttackMessageCode);
}