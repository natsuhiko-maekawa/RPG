using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IUseCase
{
    public interface ISkillElementUseCase<in TSkillElement>
    {
        public IReadOnlyList<BattleEventValueObject> GetBattleEventList(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TSkillElement> skillElementList,
            IReadOnlyList<CharacterId> targetIdList);

        public void RegisterBattleEvent(IReadOnlyList<BattleEventValueObject> battleEventList);
        public bool IsExecutedDamage();
    }
}