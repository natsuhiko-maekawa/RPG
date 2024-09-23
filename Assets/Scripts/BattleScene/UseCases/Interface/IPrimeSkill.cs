using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Interface
{
    public interface IPrimeSkill<TPrimeSkillParameter, TPrimeSkill>
    {
        public IReadOnlyList<TPrimeSkill> Commit(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameter,
            IReadOnlyList<CharacterId> targetIdList);
    }
}