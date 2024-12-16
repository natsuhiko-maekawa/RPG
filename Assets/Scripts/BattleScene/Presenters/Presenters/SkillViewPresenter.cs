using System;
using System.Linq;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;
using ActionCode = BattleScene.Views.Code.ActionCode;

namespace BattleScene.Presenters.Presenters
{
    public class SkillViewPresenter
    {
        private readonly TableView _tableView;
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly IResource<PlayerImageDto, PlayerImageCode> _playerPropertyResource;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IResource<SkillViewDto, SkillCode> _skillPropertyFactory;
        private readonly ITechnicalPointService _technicalPoint;

        public SkillViewPresenter(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IResource<SkillViewDto, SkillCode> skillPropertyFactory,
            IResource<MessageDto, MessageCode> messageResource,
            IResource<PlayerImageDto, PlayerImageCode> playerPropertyResource,
            TableView tableView,
            ITechnicalPointService technicalPoint)
        {
            _skillFactory = skillFactory;
            _skillPropertyFactory = skillPropertyFactory;
            _messageResource = messageResource;
            _playerPropertyResource = playerPropertyResource;
            _tableView = tableView;
            _technicalPoint = technicalPoint;
        }

        public void StartAnimation(BattleEventCode battleEventCode, SkillCode[] skillCodeArray)
        {
            var actionCode = battleEventCode switch
            {
                BattleEventCode.Skill => ActionCode.Skill,
                BattleEventCode.FatalitySkill => ActionCode.Fatality,
                _ => throw new ArgumentOutOfRangeException(nameof(battleEventCode), battleEventCode, null)
            };

            var rowArray = skillCodeArray
                .Select(GetRow)
                .ToArray();
            var dto = new TableViewModel(
                actionCode: actionCode,
                rowList: rowArray);
            _tableView.StartAnimation(dto);
        }

        public void StopAnimation()
        {
            _tableView.StopAnimation();
        }

        private RowModel GetRow(SkillCode x)
        {
            var skill = _skillFactory.Create(x);
            var skillProperty = _skillPropertyFactory.Get(x);
            var description = _messageResource.Get(skillProperty.Description).Message;
            var playerImagePath = _playerPropertyResource.Get(skillProperty.PlayerImageCode).Path;
            // TODO: スキル使用可否の判断で部位破壊についても考慮すること。
            var enabled = skill.Common.TechnicalPoint <= _technicalPoint.Get();
            var dto = new RowModel(
                rowName: skillProperty.SkillName,
                rowDescription: description,
                playerImagePath: playerImagePath,
                enabled: enabled,
                technicalPoint: skill.Common.TechnicalPoint);
            return dto;
        }
    }
}