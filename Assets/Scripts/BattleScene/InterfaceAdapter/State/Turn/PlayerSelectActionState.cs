﻿using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class PlayerSelectActionState : BaseState
    {
        private readonly PlayerSelectActionUseCase _useCase;
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
            PlayerSelectSkillState playerSelectSkillState,
            PlayerSelectTargetState playerSelectTargetState,
            SkillState skillState,
            GridViewPresenter gridView,
            PlayerSelectActionUseCase useCase)
        {
            _playerSelectSkillState = playerSelectSkillState;
            _playerSelectTargetState = playerSelectTargetState;
            _skillState = skillState;
            _gridView = gridView;
            _useCase = useCase;
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
                if (Context.ActorId == null) throw new InvalidOperationException();
                var oneself = _useCase.GetOneself(Context.ActorId);
                Context.TargetIdList = oneself;
            }

            Context.Skill = _useCase.GetSkill(actionCode);

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