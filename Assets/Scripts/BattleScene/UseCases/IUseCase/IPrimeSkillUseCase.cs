using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IUseCase
{
    public interface IPrimeSkillUseCase<in TPrimeSkillParameter>
    {
        public IReadOnlyList<BattleEventValueObject> GetBattleEventList(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList);

        public void RegisterBattleEvent(IReadOnlyList<BattleEventValueObject> battleEventList);
        public bool IsExecutedDamage();
    }
}