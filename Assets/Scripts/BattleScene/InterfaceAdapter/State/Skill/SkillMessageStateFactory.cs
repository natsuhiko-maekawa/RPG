﻿using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class SkillMessageStateFactory
    {
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IMessageViewPresenter _messageView;

        public SkillMessageStateFactory(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IMessageViewPresenter messageView)
        {
            _skillFactory = skillFactory;
            _battleLogRepository = battleLogRepository;
            _messageView = messageView;
        }

        public SkillMessageState Create(SkillTypeCode skillTypeCode) => new SkillMessageState(
            skillFactory: _skillFactory,
            battleLogRepository: _battleLogRepository,
            messageView: _messageView,
            skillTypeCode: skillTypeCode);
    }
}