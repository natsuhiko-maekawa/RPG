using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.IPresenter;
using ActionCode = BattleScene.Framework.Code.ActionCode;

namespace BattleScene.InterfaceAdapter.ObsoletePresenter
{
    [Obsolete]
    public class SkillViewPresenter : ISkillViewPresenter
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

        public void Start()
        {
            var actionCode = ActionCode.Skill;
            var rowDtoList = _propertyFactory.Create(CharacterTypeCode.Player).Skills
                .Select(x =>
                {
                    var skill = _skillFactory.Create(x);
                    var skillProperty = _skillPropertyFactory.Get(x);
                    var message = _messageResource.Get(skillProperty.Description).Message;
                    var description = _messageCodeConverter.Replace(message);
                    var playerImagePath = _playerPropertyResource.Get(skillProperty.PlayerImageCode).PlayerImagePath;
                    // TODO: スキル使用可否の判断で部位破壊についても考慮する
                    var enabled = skill.TechnicalPoint <= _player.Get().CurrentTechnicalPoint;
                    return new RowDto(
                        RowId: 0,
                        RowName: skillProperty.SkillName,
                        RowDescription: description,
                        PlayerImagePath: playerImagePath,
                        Enabled: enabled,
                        TechnicalPoint: skill.TechnicalPoint);
                })
                .ToImmutableList();
            var dto = new GridViewDto(
                actionCode,
                rowDtoList);
            _gridView.StartAnimationAsync(dto);
        }

        public void Stop()
        {
            _gridView.StopAnimation();
        }
    }
}