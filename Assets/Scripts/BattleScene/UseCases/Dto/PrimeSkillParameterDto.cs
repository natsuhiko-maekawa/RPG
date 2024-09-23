using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Dto
{
    public record PrimeSkillParameterDto<TPrimeSkillParameter>(
        SkillCommonValueObject SkillCommon,
        IReadOnlyList<TPrimeSkillParameter> List,
        IReadOnlyList<CharacterId> TargetIdList);
}