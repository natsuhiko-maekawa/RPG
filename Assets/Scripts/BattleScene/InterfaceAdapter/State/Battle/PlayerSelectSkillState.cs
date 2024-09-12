using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerSelectSkillState : AbstractState
    {
        private readonly IResource<CharacterPropertyDto, CharacterTypeCode> _propertyResource;
        private readonly SelectTargetStateFactory _selectTargetStateFactory;
        private readonly ISkillViewPresenter _skillView;

        public PlayerSelectSkillState(
            IResource<CharacterPropertyDto, CharacterTypeCode> propertyResource,
            SelectTargetStateFactory selectTargetStateFactory,
            ISkillViewPresenter skillView)
        {
            _propertyResource = propertyResource;
            _selectTargetStateFactory = selectTargetStateFactory;
            _skillView = skillView;
        }

        public override void Start()
        {
            _skillView.Start();
        }

        public override void Select(int id)
        {
            _skillView.Stop();
            var skillCode = _propertyResource.Get(CharacterTypeCode.Player).Skills[id];
            var selectTargetState = _selectTargetStateFactory.Create(skillCode);
            Context.TransitionTo(selectTargetState);
        }
    }
}