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
        private readonly SkillViewPresenter _skillView;

        public PlayerSelectSkillState(
            IResource<CharacterPropertyDto, CharacterTypeCode> propertyResource,
            PlayerSelectTargetState playerSelectTargetState,
            SkillViewPresenter skillView,
            PlayerSelectSkillUseCase useCase)
        {
            _propertyResource = propertyResource;
            _playerSelectTargetState = playerSelectTargetState;
            _skillView = skillView;
            _useCase = useCase;
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
            Context.TransitionTo(_playerSelectTargetState);
        }
    }
}