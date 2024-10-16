using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.IService;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class EnemySelectSkillState : BaseState
    {
        private readonly IEnemySkillSelectorService _enemySkillSelector;
        private readonly PlayerDomainService _player;
        private readonly SkillState _skillState;

        public EnemySelectSkillState(
            IEnemySkillSelectorService enemySkillSelector,
            PlayerDomainService player,
            SkillState skillState)
        {
            _enemySkillSelector = enemySkillSelector;
            _player = player;
            _skillState = skillState;
        }

        public override void Start()
        {
            Context.SkillCode = _enemySkillSelector.Select();
            Context.TargetIdList = ImmutableList.Create(_player.GetId());
            Context.TransitionTo(_skillState);
        }
    }
}