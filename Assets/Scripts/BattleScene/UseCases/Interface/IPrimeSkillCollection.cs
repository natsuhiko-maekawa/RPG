using System.Collections.Generic;

namespace BattleScene.UseCases.Interface
{
    public interface IPrimeSkillCollection<TPrimeSkillValueObject>
    {
        public void Commit(IReadOnlyList<TPrimeSkillValueObject> primeSkillList);
    }
}