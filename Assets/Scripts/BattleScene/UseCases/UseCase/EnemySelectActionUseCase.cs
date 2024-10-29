using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.IUseCase;

namespace BattleScene.UseCases.UseCase
{
    public class EnemySelectActionUseCase : IEnemySelectActionUseCase
    {
        private readonly IEnemySkillSelectorService _enemySkillSelector;
        private readonly ITargetService _target;

        public EnemySelectActionUseCase(
            IEnemySkillSelectorService enemySkillSelector,
            ITargetService target)
        {
            _enemySkillSelector = enemySkillSelector;
            _target = target;
        }

        public SkillValueObject SelectSkill(CharacterId actorId)
        {
            var skill = _enemySkillSelector.Select(actorId);
            return skill;
        }

        public IReadOnlyList<CharacterId> SelectTarget(CharacterId actorId, SkillValueObject skill)
        {
            var targetList = _target.Get(actorId, skill.Common.Range);
            return targetList;
        }
    }
}