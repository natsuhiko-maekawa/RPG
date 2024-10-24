using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IUseCase
{
    public interface IPrimeSkillUseCase<in TPrimeSkillParameter>
    {
        public IReadOnlyList<BattleEventValueObject> Commit(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList);

        public bool IsExecutedDamage();
    }
}