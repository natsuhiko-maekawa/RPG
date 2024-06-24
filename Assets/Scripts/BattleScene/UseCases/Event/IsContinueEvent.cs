using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Event.Interface;
using BattleScene.UseCases.Event.Runner;
using BattleScene.UseCases.View.IsContinueView.OutputBoundary;
using BattleScene.UseCases.View.IsContinueView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using UnityEngine;
using static BattleScene.UseCases.Event.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;
using static BattleScene.UseCases.Constant;

namespace BattleScene.UseCases.Event
{
    internal class IsContinueEvent : IEvent, IWait, ISelectable
    {
        private readonly IsContinueOutputDataFactory _isContinueOutputDataFactory;
        private readonly IIsContinueViewPresenter _isContinueView;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageViewPresenter;
        private readonly ISelectorRepository _selectorRepository;

        public IsContinueEvent(
            IsContinueOutputDataFactory isContinueOutputDataFactory,
            IIsContinueViewPresenter isContinueView,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageViewPresenter,
            ISelectorRepository selectorRepository)
        {
            _isContinueOutputDataFactory = isContinueOutputDataFactory;
            _isContinueView = isContinueView;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageViewPresenter = messageViewPresenter;
            _selectorRepository = selectorRepository;
        }

        public EventCode Run()
        {
            var selectorId = new SelectorId(EventCode.IsContinueEvent);
            var selector = new SelectorAggregate(selectorId, OptionList.Count, OptionList.Count);
            _selectorRepository.Update(selector);

            var isContinueOutputData = _isContinueOutputDataFactory.Create();
            _isContinueView.Start(isContinueOutputData);
            var messageOutputData = _messageOutputDataFactory.Create(IsContinueMessage);
            _messageViewPresenter.Start(messageOutputData);
            return WaitEvent;
        }

        public void SelectAction(Vector2 direction)
        {
            var selector = _selectorRepository.Select(new SelectorId(EventCode.IsContinueEvent));
            switch (direction.y)
            {
                case > 0: // 上入力時
                    selector.Up();
                    break;
                case < 0: // 下入力時
                    selector.Down();

                    break;
            }

            _selectorRepository.Update(selector);
            var isContinueOutputData = _isContinueOutputDataFactory.Create();
            _isContinueView.Start(isContinueOutputData);
        }

        public EventCode NextEvent()
        {
            _isContinueView.Stop();
            var selector = _selectorRepository.Select(new SelectorId(EventCode.IsContinueEvent));
            switch (selector.GetSelection())
            {
                case 0:
                    return EventCode.InitializationEvent;
                case 1:
                    Application.Quit();
                    break;
            }

            return WaitEvent;
        }
    }
}