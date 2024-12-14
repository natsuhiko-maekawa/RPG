using System;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.PresenterFacades;
using BattleScene.UseCases.UseCases;

namespace BattleScene.Presenters.States.Turn
{
    public class PlayerSelectSkillState : BaseState, ICancelable
    {
        private readonly IResource<CharacterPropertyDto, CharacterTypeCode> _characterPropertyResource;
        private readonly PlayerSelectSkillUseCase _useCase;
        private readonly PlayerSelectSkillPresenterFacade _facade;
        private readonly PlayerSelectTargetState _playerSelectTargetState;
        private readonly SkillState _skillState;

        public PlayerSelectSkillState(
            IResource<CharacterPropertyDto, CharacterTypeCode> characterPropertyResource,
            PlayerSelectSkillUseCase useCase,
            PlayerSelectSkillPresenterFacade facade,
            PlayerSelectTargetState playerSelectTargetState,
            SkillState skillState)
        {
            _characterPropertyResource = characterPropertyResource;
            _useCase = useCase;
            _facade = facade;
            _playerSelectTargetState = playerSelectTargetState;
            _skillState = skillState;
        }

        public override void Start()
        {
            var skillCodeArray = _useCase.GetSkillCodeArray(Context.BattleEventCode);
            _facade.Output(Context.BattleEventCode, skillCodeArray);
        }

        public override void Select(int id)
        {
            _facade.Stop();
            var skillCode = _useCase.GetSkillCodeArray(Context.BattleEventCode)[id];
            Context.Skill = _useCase.GetSkill(skillCode);
            if (Context.Skill.Common.IsAutoTarget)
            {
                var actor = Context.Actor ?? throw new InvalidOperationException();
                Context.TargetList = _useCase.GetTarget(actor, Context.Skill.Common.Range);
                Context.TransitionTo(_skillState);
            }
            else
            {
                Context.TransitionTo(_playerSelectTargetState);
            }
        }

        public void OnCancel()
        {
            _facade.Stop();
        }
    }
}