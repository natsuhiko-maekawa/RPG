using System.Collections.Generic;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.UseCases
{
    public class EnemySelectActionUseCase
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

        public SkillValueObject SelectSkill(CharacterEntity actor)
        {
            var skill = _enemySkillSelector.Select(actor);
            return skill;
        }

        public IReadOnlyList<CharacterEntity> SelectTarget(CharacterEntity actor, SkillValueObject skill)
        {
            var targetList = _target.Get(actor, skill.Common.Range);
            return targetList;
        }
    }
}