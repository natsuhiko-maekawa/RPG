using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerSelectActionState : AbstractState
    {
        private readonly PlayerDomainService _player;
        private readonly PlayerSelectSkillState _playerSelectSkillState;
        private readonly SelectTargetStateFactory _selectTargetStateFactory;
        private readonly SkillStateFactory _skillStateFactory;
        private readonly GridViewPresenter _gridView;

        public PlayerSelectActionState(
            PlayerDomainService player,
            PlayerSelectSkillState playerSelectSkillState,
            SelectTargetStateFactory selectTargetStateFactory,
            SkillStateFactory skillStateFactory,
            GridViewPresenter gridView)
        {
            _player = player;
            _playerSelectSkillState = playerSelectSkillState;
            _selectTargetStateFactory = selectTargetStateFactory;
            _skillStateFactory = skillStateFactory;
            _gridView = gridView;
        }

        public override void Start()
        {
            _gridView.StartAnimationAsync();
        }

        public override void Select(int id)
        {
            var actionCode = (ActionCode)id;
            var oneself = ImmutableList.Create(_player.GetId());
            AbstractState nextState = actionCode switch
            {
                ActionCode.Attack => _selectTargetStateFactory.Create(SkillCode.Attack),
                ActionCode.Skill => _playerSelectSkillState,
                ActionCode.Defence => _skillStateFactory.Create(SkillCode.Defence, oneself),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _gridView.Stop();
            Context.TransitionTo(nextState);
        }
    }
}