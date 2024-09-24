using System.Collections.Generic;

namespace BattleScene.UseCases.IService
{
    public interface IPrimeSkillRegistererService<TPrimeSkill>
    {
        public void Register(IReadOnlyList<TPrimeSkill> primeSkillList);
    }
}