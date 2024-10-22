using System.Collections.Generic;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface IPrimeSkillRegistererService<TPrimeSkill>
    {
        public void Register(IReadOnlyList<TPrimeSkill> primeSkillList);
    }
    
    // IPrimeSkillRegistererServiceを実装するすべてのクラスの修正が完了するまで必要
    public class TempPrimeSkillRegistererService : IPrimeSkillRegistererService<PrimeSkillValueObject>
    {
        public void Register(IReadOnlyList<PrimeSkillValueObject> primeSkillList) { }
    }
}