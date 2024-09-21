using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class BuffMessageState : AbstractSkillState
    {
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IResource<BuffViewDto, BuffCode> _buffViewResource;
        private readonly MessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public BuffMessageState(
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IResource<BuffViewDto, BuffCode> buffViewResource,
            MessageViewPresenter messageView,
            SkillEndState skillEndState)
        {
            _battleLogRepository = battleLogRepository;
            _buffViewResource = buffViewResource;
            _messageView = messageView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            var buffCode = _battleLogRepository.Select().Max().BuffCode;
            var messageCode = _buffViewResource.Get(buffCode).MessageCode;
            _messageView.StartMessageAnimationAsync(messageCode);
        }

        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}