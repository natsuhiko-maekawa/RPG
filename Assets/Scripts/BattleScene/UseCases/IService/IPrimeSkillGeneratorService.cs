using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface IPrimeSkillGeneratorService<TPrimeSkillParameter, TPrimeSkill>
    {
        public IReadOnlyList<TPrimeSkill> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList);
    }
}