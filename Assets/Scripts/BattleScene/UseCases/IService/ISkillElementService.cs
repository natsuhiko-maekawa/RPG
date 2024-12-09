using System.Collections.Generic;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface ISkillElementService<in TSkillElement>
    {
        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> battleEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TSkillElement> skillElementList,
            IReadOnlyList<CharacterId> targetIdList);

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> battleEventList);
    }
}