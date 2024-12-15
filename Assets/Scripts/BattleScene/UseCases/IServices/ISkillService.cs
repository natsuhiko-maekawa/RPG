using System.Collections.Generic;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;

namespace BattleScene.UseCases.IServices
{
    public interface ISkillService<in TSkillComponent>
    {
        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> battleEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TSkillComponent> skillComponentList,
            IReadOnlyList<CharacterEntity> targetList);

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> battleEventList);
    }
}