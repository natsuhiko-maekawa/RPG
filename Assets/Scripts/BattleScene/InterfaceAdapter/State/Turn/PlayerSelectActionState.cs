using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PresenterFacade;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class PlayerSelectActionState : BaseState
    {
        private readonly PlayerSelectActionUseCase _useCase;
        private readonly PlayerSelectSkillState _playerSelectSkillState;
        private readonly PlayerSelectTargetState _playerSelectTargetState;
        private readonly SkillState _skillState;
        private readonly PlayerSelectActionPresenterFacade _facade;

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
            PlayerSelectActionUseCase useCase,
            PlayerSelectActionPresenterFacade facade)
        {
            _playerSelectSkillState = playerSelectSkillState;
            _playerSelectTargetState = playerSelectTargetState;
            _skillState = skillState;
            _useCase = useCase;
            _facade = facade;
        }

        public override void Start()
        {
            _facade.Output();
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

            _facade.Stop();
            Context.TransitionTo(nextState);
        }
    }
}