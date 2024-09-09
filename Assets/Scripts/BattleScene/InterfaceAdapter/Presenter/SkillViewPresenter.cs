using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.InterfaceAdapter.Presenter.Dto;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.IPresenter;
using ActionCode = BattleScene.InterfaceAdapter.Code.ActionCode;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class SkillViewPresenter : ISkillViewPresenter
    {
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IGridView _gridView;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly PlayerDomainService _player;
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _propertyFactory;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IResource<SkillViewInfoValueObject, SkillCode> _skillPropertyFactory;

        public SkillViewPresenter(
            PlayerDomainService player,
            IFactory<PropertyValueObject, CharacterTypeCode> propertyFactory,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IResource<SkillViewInfoValueObject, SkillCode> skillPropertyFactory,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IResource<MessageDto, MessageCode> messageResource,
            MessageCodeConverterService messageCodeConverter,
            IGridView gridView)
        {
            _player = player;
            _propertyFactory = propertyFactory;
            _skillFactory = skillFactory;
            _skillPropertyFactory = skillPropertyFactory;
            _characterRepository = characterRepository;
            _messageResource = messageResource;
            _messageCodeConverter = messageCodeConverter;
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
                    var enabled = skill.TechnicalPoint <= _player.Get().CurrentTechnicalPoint;
                    return new RowDto(
                        RowId: 0,
                        RowName: skillProperty.SkillName,
                        RowDescription: description,
                        PlayerImagePath: "",
                        Enabled: enabled,
                        TechnicalPoint: skill.TechnicalPoint);
                })
                .ToImmutableList();
            var dto = new GridViewDto(
                actionCode,
                rowDtoList);
            _gridView.StartAnimationAsync(dto);
        }
    }
}