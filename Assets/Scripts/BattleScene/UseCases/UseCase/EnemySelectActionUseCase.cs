using System.Collections.Generic;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.UseCase
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