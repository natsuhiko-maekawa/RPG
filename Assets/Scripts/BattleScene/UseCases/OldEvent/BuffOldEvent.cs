using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.BuffView.OutputBoundary;
using BattleScene.UseCases.View.BuffView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class BuffOldEvent : IOldEvent, IWait
    {
        private readonly BuffOutputDataFactory _buffOutputDataFactory;
        private readonly IBuffViewPresenter _buffView;
        private readonly IBuffViewInfoFactory _buffViewInfoFactory;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly ResultDomainService _result;

        public BuffOldEvent(
            BuffOutputDataFactory buffOutputDataFactory,
            IBuffViewPresenter buffView,
            IBuffViewInfoFactory buffViewInfoFactory,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            ResultDomainService result)
        {
            _buffOutputDataFactory = buffOutputDataFactory;
            _buffView = buffView;
            _buffViewInfoFactory = buffViewInfoFactory;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _result = result;
        }

        public EventCode Run()
        {
            // if (_skillRepository.Select(_orderedItems.FirstCharacterId()).Skill is not IBuffSkill)
            //     throw new InvalidCastException();

            var buffSkillResult = _result.Last<BuffValueObject>();

            var buffOutputData = _buffOutputDataFactory.Create(buffSkillResult.TargetIdList);
            _buffView.Start(buffOutputData);

            var buffViewInfo = _buffViewInfoFactory.Create(buffSkillResult.BuffCode);
            var messageOutputData = _messageOutputDataFactory.Create(buffViewInfo.MessageCode);
            _messageView.Start(messageOutputData);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.LoopEndEvent;
        }
    }
}