using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    [Obsolete]
    public class SkillMessageStateFactory
    {
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IMessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public SkillMessageStateFactory(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IMessageViewPresenter messageView,
            SkillEndState skillEndState)
        {
            _skillFactory = skillFactory;
            _battleLogRepository = battleLogRepository;
            _messageView = messageView;
            _skillEndState = skillEndState;
        }

        public SkillMessageState Create(SkillTypeCode skillTypeCode) => new SkillMessageState(
            skillFactory: _skillFactory,
            battleLogRepository: _battleLogRepository,
            messageView: _messageView,
            skillEndState: _skillEndState);
    }
}