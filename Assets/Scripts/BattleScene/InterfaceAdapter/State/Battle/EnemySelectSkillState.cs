using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.Service;
using VContainer;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class EnemySelectSkillState : AbstractState
    {
        private readonly EnemySkillSelectorService _enemySkillSelector;
        private readonly PlayerDomainService _player;
        private readonly IObjectResolver _container;

        public EnemySelectSkillState(
            EnemySkillSelectorService enemySkillSelector,
            PlayerDomainService player,
            IObjectResolver container)
        {
            _enemySkillSelector = enemySkillSelector;
            _player = player;
            _container = container;
        }

        public override void Start()
        {
            var skillCode = _enemySkillSelector.Select();
            var targetIdList = ImmutableList.Create(_player.GetId());
            var nextState = _container.Resolve<SkillStateFactory>().Create(skillCode, targetIdList);
            Context.TransitionTo(nextState);
        }
    }
}