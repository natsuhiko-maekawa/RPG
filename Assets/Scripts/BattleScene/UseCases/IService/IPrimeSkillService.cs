using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface IPrimeSkillService<in TPrimeSkillParameter>
    {
        public IReadOnlyList<BattleEventValueObject> Generate(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList);

        public void Register(IReadOnlyList<BattleEventValueObject> primeSkillList);
    }
}