using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class PlayerSelectActionState : BaseState
    {
        private readonly PlayerDomainService _player;
        private readonly PlayerSelectSkillState _playerSelectSkillState;
        private readonly PlayerSelectTargetState _playerSelectTargetState;
        private readonly SkillState _skillState;
        private readonly GridViewPresenter _gridView;

        private readonly IReadOnlyDictionary<int, ActionCode> _actionCodeDictionary = new Dictionary<int, ActionCode>()
        {
            { 0, ActionCode.Attack },
            { 1, ActionCode.Skill },
            { 2, ActionCode.Defence },
            { 3, ActionCode.FatalitySkill }
        };

        public PlayerSelectActionState(
            PlayerDomainService player,
            PlayerSelectSkillState playerSelectSkillState,
            PlayerSelectTargetState playerSelectTargetState,
            SkillState skillState,
            GridViewPresenter gridView)
        {
            _player = player;
            _playerSelectSkillState = playerSelectSkillState;
            _playerSelectTargetState = playerSelectTargetState;
            _skillState = skillState;
            _gridView = gridView;
        }

        public override void Start()
        {
            _gridView.StartAnimationAsync();
        }

        public override void Select(int id)
        {
            var actionCode = _actionCodeDictionary[id];

            if (actionCode == ActionCode.Defence)
            {
                var oneself = new[] { _player.GetId() };
                Context.TargetIdList = oneself;
            }

            Context.SkillCode = actionCode switch
            {
                ActionCode.Attack => SkillCode.Attack,
                ActionCode.Defence => SkillCode.Defence,
                _ => SkillCode.NoSkill
            };

            BaseState nextState = actionCode switch
            {
                ActionCode.Attack => _playerSelectTargetState,
                ActionCode.Skill => _playerSelectSkillState,
                ActionCode.Defence => _skillState,
                _ => throw new ArgumentOutOfRangeException()
            };

            _gridView.Stop();
            Context.TransitionTo(nextState);
        }
    }
}