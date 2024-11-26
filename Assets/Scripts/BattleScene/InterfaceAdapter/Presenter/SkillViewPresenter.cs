using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.UseCases.IService;
using ActionCode = BattleScene.Framework.Code.ActionCode;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class SkillViewPresenter
    {
        private readonly GridView _gridView;
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly IResource<PlayerImageDto, PlayerImageCode> _playerPropertyResource;
        private readonly IFactory<CharacterPropertyValueObject, CharacterTypeCode> _propertyFactory;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IResource<SkillViewDto, SkillCode> _skillPropertyFactory;
        private readonly ITechnicalPointService _technicalPoint;

        public SkillViewPresenter(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> propertyFactory,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IResource<SkillViewDto, SkillCode> skillPropertyFactory,
            IResource<MessageDto, MessageCode> messageResource,
            IResource<PlayerImageDto, PlayerImageCode> playerPropertyResource,
            GridView gridView,
            ITechnicalPointService technicalPoint)
        {
            _propertyFactory = propertyFactory;
            _skillFactory = skillFactory;
            _skillPropertyFactory = skillPropertyFactory;
            _messageResource = messageResource;
            _playerPropertyResource = playerPropertyResource;
            _gridView = gridView;
            _technicalPoint = technicalPoint;
        }

        public async void StartAnimationAsync()
        {
            var actionCode = ActionCode.Skill;
            var rowDtoList = _propertyFactory.Create(CharacterTypeCode.Player).SkillCodeList
                .Select(GetRowDto)
                .ToList();
            var dto = new GridViewDto(
                ActionCode: actionCode,
                RowDtoList: rowDtoList);
            await _gridView.StartAnimationAsync(dto);
        }

        public void StopAnimation()
        {
            _gridView.StopAnimation();
        }

        private RowDto GetRowDto(SkillCode x)
        {
            var skill = _skillFactory.Create(x);
            var skillProperty = _skillPropertyFactory.Get(x);
            var description = _messageResource.Get(skillProperty.Description).Message;
            var playerImagePath = _playerPropertyResource.Get(skillProperty.PlayerImageCode).Path;
            // TODO: スキル使用可否の判断で部位破壊についても考慮する
            var enabled = skill.Common.TechnicalPoint <= _technicalPoint.Get();
            var dto = new RowDto(
                RowId: 0,
                RowName: skillProperty.SkillName,
                RowDescription: description,
                PlayerImagePath: playerImagePath,
                Enabled: enabled,
                TechnicalPoint: skill.Common.TechnicalPoint);
            return dto;
        }
    }
}