using System;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class PlayerSelectSkillState : BaseState
    {
        private readonly IResource<CharacterPropertyDto, CharacterTypeCode> _propertyResource;
        private readonly PlayerSelectSkillUseCase _useCase;
        private readonly PlayerSelectTargetState _playerSelectTargetState;
        private readonly SkillState _skillState;
        private readonly SkillViewPresenter _skillView;

        public PlayerSelectSkillState(
            IResource<CharacterPropertyDto, CharacterTypeCode> propertyResource,
            PlayerSelectTargetState playerSelectTargetState,
            SkillViewPresenter skillView,
            PlayerSelectSkillUseCase useCase,
            SkillState skillState)
        {
            _propertyResource = propertyResource;
            _playerSelectTargetState = playerSelectTargetState;
            _skillView = skillView;
            _useCase = useCase;
            _skillState = skillState;
        }

        public override void Start()
        {
            _skillView.StartAnimationAsync();
        }

        public override void Select(int id)
        {
            _skillView.StopAnimation();
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
    }
}