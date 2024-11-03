using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface ISkillElementService<in TSkillElement>
    {
        public IReadOnlyList<BattleEventValueObject> GenerateBattleEvent(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TSkillElement> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList);

        public void RegisterBattleEvent(IReadOnlyList<BattleEventValueObject> battleEventList);
    }
}