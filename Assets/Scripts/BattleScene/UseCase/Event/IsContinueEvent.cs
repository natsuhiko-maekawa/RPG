using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.View.IsContinueView.OutputBoundary;
using BattleScene.UseCase.View.IsContinueView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using UnityEngine;
using static BattleScene.UseCase.EventRunner.EventCode;
using static BattleScene.Domain.Code.MessageCode;
using static BattleScene.UseCase.Constant;

namespace BattleScene.UseCase.Event
{
    internal class IsContinueEvent : IEvent, IWait, ISelectable
    {
        private readonly IsContinueOutputDataFactory _isContinueOutputDataFactory;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly ISelectorRepository _selectorRepository;
        private readonly IIsContinueViewPresenter _isContinueView;
        private readonly IMessageViewPresenter _messageViewPresenter;
        
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
    }
}