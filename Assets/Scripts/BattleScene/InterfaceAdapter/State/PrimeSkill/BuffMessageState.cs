using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public class BuffMessageState : PrimeSkillOutputState<BuffParameterValueObject, BuffValueObject>
    {
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IResource<BuffViewDto, BuffCode> _buffViewResource;
        private readonly MessageViewPresenter _messageView;
        private readonly PrimeSkillStopState<BuffParameterValueObject, BuffValueObject> _primeSkillStopState;

        public BuffMessageState(
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IResource<BuffViewDto, BuffCode> buffViewResource,
            MessageViewPresenter messageView,
            PrimeSkillStopState<BuffParameterValueObject, BuffValueObject> primeSkillStopState)
        {
            _battleLogRepository = battleLogRepository;
            _buffViewResource = buffViewResource;
            _messageView = messageView;
            _primeSkillStopState = primeSkillStopState;
        }

        public override void Start()
        {
            var buffCode = _battleLogRepository.Select().Max().BuffCode;
            var messageCode = _buffViewResource.Get(buffCode).MessageCode;
            _messageView.StartMessageAnimationAsync(messageCode);
        }

        public override void Select()
        {
            Context.TransitionTo(_primeSkillStopState);
        }
    }
}