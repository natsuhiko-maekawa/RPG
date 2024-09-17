using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerSelectSkillState : AbstractState
    {
        private readonly IResource<CharacterPropertyDto, CharacterTypeCode> _propertyResource;
        private readonly SelectTargetStateFactory _selectTargetStateFactory;
        private readonly SkillViewPresenter _skillView;

        public PlayerSelectSkillState(
            IResource<CharacterPropertyDto, CharacterTypeCode> propertyResource,
            SelectTargetStateFactory selectTargetStateFactory,
            SkillViewPresenter skillView)
        {
            _propertyResource = propertyResource;
            _selectTargetStateFactory = selectTargetStateFactory;
            _skillView = skillView;
        }

        public override void Start()
        {
            _skillView.StartAnimationAsync();
        }

        public override void Select(int id)
        {
            _skillView.StopAnimation();
            var skillCode = _propertyResource.Get(CharacterTypeCode.Player).Skills[id];
            var selectTargetState = _selectTargetStateFactory.Create(skillCode);
            Context.TransitionTo(selectTargetState);
        }
    }
}