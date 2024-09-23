using System.Collections.Generic;
using BattleScene.UseCases.Dto;

namespace BattleScene.UseCases.Interface
{
    public interface IPrimeSkill<TPrimeSkillParameter, TPrimeSkill>
    {
        public IReadOnlyList<TPrimeSkill> Commit(PrimeSkillParameterDto<TPrimeSkillParameter> primeSkillParameter);
    }
}