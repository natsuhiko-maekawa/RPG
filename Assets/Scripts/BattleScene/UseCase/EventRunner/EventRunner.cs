using System;
using System.Collections.Generic;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.IController;
using UnityEngine;
using static BattleScene.UseCase.EventRunner.EventCode;

namespace BattleScene.UseCase.EventRunner
{
    internal class EventRunner
    {
        private readonly IEventFactory _eventFactory;
        private readonly IBattleSceneController _battleSceneController;
        private IEvent _event;
        private readonly Stack<EventCode> _history = new();

        public EventRunner(
            IEventFactory eventFactory,
            IBattleSceneController battleSceneController)
        {
            _eventFactory = eventFactory;
            _battleSceneController = battleSceneController;
            _history.Push(InitializationEvent);
            SetInput();
        }
        
        public void Run()
        {
            if (_history.Count == 0) throw new InvalidOperationException();
            var index =  _history.Peek();
            if (index == WaitEvent) return;
            _event = _eventFactory.Create(index);
            var nextIndex = _event.Run();
            _history.Push(nextIndex);
        }

        private void SetInput()
        {
            _battleSceneController.SetOnNextAction(OnNextAction);
            _battleSceneController.SetOnCancelAction(OnCancelAction);
            _battleSceneController.SetOnSelectAction(OnSelectAction);
        }

        private void OnNextAction()
        {
            if (_event is not IWait waitEvent) return;
            var index = waitEvent.NextEvent();
            _history.Pop(); // WaitEventを削除
            _history.Push(index);
        }

        private void OnCancelAction()
        {
            if (_event is not ICancelable cancelableEvent) return;
            cancelableEvent.CancelAction();
            _history.Pop(); // WaitEventを削除
            _history.Pop(); // 自分自身を削除
        }

        private void OnSelectAction(Vector2 vector2)
        {
            if (_event is not ISelectable selectableEvent) return;
            selectableEvent.SelectAction(vector2);
        }
    }
}