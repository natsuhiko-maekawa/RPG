using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.State.Skill;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerSelectActionState : AbstractState
    {
        private readonly PlayerDomainService _player;
        private readonly PlayerSelectSkillState _playerSelectSkillState;
        private readonly SelectTargetState _selectTargetState;
        private readonly SkillState _skillState;
        private readonly GridViewPresenter _gridView;

        public PlayerSelectActionState(
            PlayerDomainService player,
            PlayerSelectSkillState playerSelectSkillState,
            SelectTargetState selectTargetState,
            SkillState skillState,
            GridViewPresenter gridView)
        {
            _player = player;
            _playerSelectSkillState = playerSelectSkillState;
            _selectTargetState = selectTargetState;
            _skillState = skillState;
            _gridView = gridView;
        }

        public override void Start()
        {
            _gridView.StartAnimationAsync();
        }

        public override void Select(int id)
        {
            var actionCode = (ActionCode)id;

            if (actionCode == ActionCode.Defence)
            {
                var oneself = ImmutableList.Create(_player.GetId());
                Context.TargetIdList = oneself;
            }
            
            Context.SkillCode = actionCode switch
            {
                ActionCode.Attack => SkillCode.Attack,
                ActionCode.Defence => SkillCode.Defence,
                _ => SkillCode.NoSkill
            };
            
            AbstractState nextState = actionCode switch
            {
                ActionCode.Attack => _selectTargetState,
                ActionCode.Skill => _playerSelectSkillState,
                ActionCode.Defence => _skillState,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _gridView.Stop();
            Context.TransitionTo(nextState);
        }
    }
}