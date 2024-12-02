using System;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PresenterFacade;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class PlayerSelectSkillState : BaseState, ICancelable
    {
        private readonly IResource<CharacterPropertyDto, CharacterTypeCode> _propertyResource;
        private readonly PlayerSelectSkillUseCase _useCase;
        private readonly PlayerSelectSkillPresenterFacade _facade;
        private readonly PlayerSelectTargetState _playerSelectTargetState;
        private readonly SkillState _skillState;

        public PlayerSelectSkillState(
            IResource<CharacterPropertyDto, CharacterTypeCode> propertyResource,
            PlayerSelectSkillUseCase useCase,
            PlayerSelectSkillPresenterFacade facade,
            PlayerSelectTargetState playerSelectTargetState,
            SkillState skillState)
        {
            _propertyResource = propertyResource;
            _useCase = useCase;
            _facade = facade;
            _playerSelectTargetState = playerSelectTargetState;
            _skillState = skillState;
        }

        public override void Start()
        {
            _facade.Output();
        }

        public override void Select(int id)
        {
            _facade.Stop();
            var skillCode = _propertyResource.Get(CharacterTypeCode.Player).SkillCodeList[id];
            Context.Skill = _useCase.GetSkill(skillCode);
            if (Context.Skill.Common.IsAutoTarget)
            {
                if (Context.ActorId is null) throw new InvalidOperationException();
                Context.TargetIdList = _useCase.GetTarget(Context.ActorId, Context.Skill.Common.Range);
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