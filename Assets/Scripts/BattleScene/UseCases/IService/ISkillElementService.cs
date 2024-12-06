using System.Collections.Generic;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface ISkillElementService<in TSkillElement>
    {
        // public IReadOnlyList<BattleEventEntity> GenerateBattleEvent(
        //     CharacterId actorId,
        //     SkillCommonValueObject skillCommon,
        //     IReadOnlyList<TSkillElement> primeSkillParameterList,
        //     IReadOnlyList<CharacterId> targetIdList);

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> buffEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TSkillElement> skillElementList,
            IReadOnlyList<CharacterId> targetIdList);

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> battleEventList);
    }
}