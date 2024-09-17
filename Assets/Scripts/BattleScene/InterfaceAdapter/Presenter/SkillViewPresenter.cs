using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.Service;
using ActionCode = BattleScene.Framework.Code.ActionCode;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class SkillViewPresenter
    {
        private readonly GridView _gridView;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly IResource<PlayerImagePathDto, PlayerImageCode> _playerPropertyResource;
        private readonly PlayerDomainService _player;
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _propertyFactory;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IResource<SkillPropertyDto, SkillCode> _skillPropertyFactory;

        public SkillViewPresenter(
            PlayerDomainService player,
            IFactory<PropertyValueObject, CharacterTypeCode> propertyFactory,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IResource<SkillPropertyDto, SkillCode> skillPropertyFactory,
            IResource<MessageDto, MessageCode> messageResource,
            IResource<PlayerImagePathDto, PlayerImageCode> playerPropertyResource,
            MessageCodeConverterService messageCodeConverter,
            GridView gridView)
        {
            _player = player;
            _propertyFactory = propertyFactory;
            _skillFactory = skillFactory;
            _skillPropertyFactory = skillPropertyFactory;
            _messageResource = messageResource;
            _messageCodeConverter = messageCodeConverter;
            _playerPropertyResource = playerPropertyResource;
            _gridView = gridView;
        }

        public async void StartAnimationAsync()
        {
            var actionCode = ActionCode.Skill;
            var rowDtoList = _propertyFactory.Create(CharacterTypeCode.Player).Skills
                .Select(GetRowDto)
                .ToImmutableList();
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
            var message = _messageResource.Get(skillProperty.Description).Message;
            var description = _messageCodeConverter.Replace(message);
            var playerImagePath = _playerPropertyResource.Get(skillProperty.PlayerImageCode).PlayerImagePath;
            // TODO: スキル使用可否の判断で部位破壊についても考慮する
            var enabled = skill.SkillCommon.TechnicalPoint <= _player.Get().CurrentTechnicalPoint;
            var dto = new RowDto(
                RowId: 0,
                RowName: skillProperty.SkillName,
                RowDescription: description,
                PlayerImagePath: playerImagePath,
                Enabled: enabled,
                TechnicalPoint: skill.SkillCommon.TechnicalPoint);
            return dto;
        }
    }
}