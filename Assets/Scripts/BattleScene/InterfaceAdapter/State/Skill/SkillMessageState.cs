using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class SkillMessageState : AbstractSkillState
    {
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IMessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public SkillMessageState(
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

        public override void Start()
        {
            var battleLog = _battleLogRepository.Select()
                .Max(x => x);
            var skill = _skillFactory.Create(battleLog.SkillCode);
            _messageView.Start(skill.SkillCommon.MessageCode);
        }

        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}