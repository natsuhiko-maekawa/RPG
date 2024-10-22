using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface IPrimeSkillService<in TPrimeSkillParameter>
    {
        public IReadOnlyList<PrimeSkillValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList);

        public void Register(IReadOnlyList<PrimeSkillValueObject> primeSkillList);
    }
}