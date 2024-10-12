using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    internal class PlayerSelectSkillState : BaseState
    {
        private readonly IResource<CharacterPropertyDto, CharacterTypeCode> _propertyResource;
        private readonly SelectTargetState _selectTargetState;
        private readonly SkillViewPresenter _skillView;

        public PlayerSelectSkillState(
            IResource<CharacterPropertyDto, CharacterTypeCode> propertyResource,
            SelectTargetState selectTargetState,
            SkillViewPresenter skillView)
        {
            _propertyResource = propertyResource;
            _selectTargetState = selectTargetState;
            _skillView = skillView;
        }

        public override void Start()
        {
            _skillView.StartAnimationAsync();
        }

        public override void Select(int id)
        {
            _skillView.StopAnimation();
            Context.SkillCode = _propertyResource.Get(CharacterTypeCode.Player).SkillCodeList[id];
            Context.TransitionTo(_selectTargetState);
        }
    }
}