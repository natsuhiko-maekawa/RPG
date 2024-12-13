using System.Collections.Generic;
using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public record SkillCommonValueObject(
        SkillCode SkillCode,
        int TechnicalPoint,
        IReadOnlyList<BodyPartCode> DependencyList,
        Range Range,
        bool IsAutoTarget,
        bool IsFatality,
        MessageCode AttackMessageCode);
}