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

        private readonly IReadOnlyDictionary<int, BattleEventCode> _actionCodeDictionary = new Dictionary<int, BattleEventCode>()
        {
            { 0, BattleEventCode.Attack },
            { 1, BattleEventCode.Skill },
            { 2, BattleEventCode.Defence },
            { 3, BattleEventCode.FatalitySkill }
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

            if (actionCode == BattleEventCode.Defence)
            {
                var oneself = new[] { _player.GetId() };
                Context.TargetIdList = oneself;
            }

            Context.SkillCode = actionCode switch
            {
                BattleEventCode.Attack => SkillCode.Attack,
                BattleEventCode.Defence => SkillCode.Defence,
                _ => SkillCode.NoSkill
            };

            BaseState nextState = actionCode switch
            {
                BattleEventCode.Attack => _playerSelectTargetState,
                BattleEventCode.Skill => _playerSelectSkillState,
                BattleEventCode.Defence => _skillState,
                _ => throw new ArgumentOutOfRangeException()
            };

            _gridView.Stop();
            Context.TransitionTo(nextState);
        }
    }
}