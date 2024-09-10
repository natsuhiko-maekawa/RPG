﻿using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.OutputData;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerSelectActionState : AbstractState
    {
        private readonly AttackCounterService _attackCounter;
        private readonly PlayerDomainService _player;
        private readonly PlayerSelectSkillState _playerSelectSkillState;
        private readonly SelectTargetStateFactory _selectTargetStateFactory;
        private readonly SkillStateFactory _skillStateFactory;
        private readonly IViewPresenter<GridViewOutputData> _gridView;

        public PlayerSelectActionState(
            AttackCounterService attackCounter,
            PlayerDomainService player,
            PlayerSelectSkillState playerSelectSkillState,
            SelectTargetStateFactory selectTargetStateFactory,
            SkillStateFactory skillStateFactory,
            IViewPresenter<GridViewOutputData> gridView)
        {
            _attackCounter = attackCounter;
            _player = player;
            _playerSelectSkillState = playerSelectSkillState;
            _selectTargetStateFactory = selectTargetStateFactory;
            _skillStateFactory = skillStateFactory;
            _gridView = gridView;
        }

        public override void Start()
        {
            var fatalitySkillEnabled = _attackCounter.IsOverflow();
            var rowList = ImmutableList.Create(
                new Row(ActionCode: ActionCode.Attack, Enabled: true),
                new Row(ActionCode: ActionCode.Skill, Enabled: true),
                new Row(ActionCode: ActionCode.Defence, Enabled: true),
                new Row(ActionCode: ActionCode.FatalitySkill, Enabled: fatalitySkillEnabled));
            var outputData = new GridViewOutputData(rowList);
            _gridView.Start(outputData);
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