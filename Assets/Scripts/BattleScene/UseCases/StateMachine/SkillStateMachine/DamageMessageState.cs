using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.UseCases.StateMachine.SkillStateMachine
{
    public class DamageMessageState : AbstractSkillState
    {
        private readonly IFactory<SkillViewInfoValueObject, SkillCode> _skillViewInfoFactory;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IMessageViewPresenter _messageView;

        public DamageMessageState(
            IFactory<SkillViewInfoValueObject, SkillCode> skillViewInfoFactory,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IMessageViewPresenter messageView)
        {
            _skillViewInfoFactory = skillViewInfoFactory;
            _battleLogRepository = battleLogRepository;
            _messageView = messageView;
        }

        public override void Start()
        {
            var battleLog = _battleLogRepository.Select()
                .Max(x => x);
            var skillViewInfo = _skillViewInfoFactory.Create(battleLog.SkillCode);
            _messageView.Start(skillViewInfo.MessageCode);
        }
    }
}