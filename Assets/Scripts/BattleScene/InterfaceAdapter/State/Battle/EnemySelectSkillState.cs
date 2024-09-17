using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.IService;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class EnemySelectSkillState : AbstractState
    {
        private readonly IEnemySkillSelectorService _enemySkillSelector;
        private readonly PlayerDomainService _player;
        private readonly SkillStateFactory _skillStateFactory;

        public EnemySelectSkillState(
            IEnemySkillSelectorService enemySkillSelector,
            PlayerDomainService player,
            SkillStateFactory skillStateFactory)
        {
            _enemySkillSelector = enemySkillSelector;
            _player = player;
            _skillStateFactory = skillStateFactory;
        }

        public override void Start()
        {
            var skillCode = _enemySkillSelector.Select();
            var targetIdList = ImmutableList.Create(_player.GetId());
            var nextState = _skillStateFactory.Create(skillCode, targetIdList);
            Context.TransitionTo(nextState);
        }
    }
}