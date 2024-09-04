using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.UseCases.View.MessageView.OutputBoundary;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class BuffMessageState : AbstractSkillState
    {
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IResource<BuffViewInfoDto, BuffCode> _buffViewInfoResource;
        private readonly IMessageViewPresenter _messageView;
        private readonly SkillEndState _skillEndState;

        public BuffMessageState(
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IResource<BuffViewInfoDto, BuffCode> buffViewInfoResource,
            IMessageViewPresenter messageView,
            SkillEndState skillEndState)
        {
            _battleLogRepository = battleLogRepository;
            _buffViewInfoResource = buffViewInfoResource;
            _messageView = messageView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            var buffCode = _battleLogRepository.Select().Max().BuffCode;
            var messageCode = _buffViewInfoResource.Get(buffCode).MessageCode;
            _messageView.Start(messageCode);
        }

        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}