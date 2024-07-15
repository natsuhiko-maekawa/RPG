using System;
using System.Collections.Generic;
using BattleScene.UseCases.IController;
using BattleScene.UseCases.OldEvent.Interface;
using UnityEngine;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent.Runner
{
    internal class EventRunner
    {
        private readonly IBattleSceneController _battleSceneController;
        private readonly IEventFactory _eventFactory;
        private readonly Stack<EventCode> _history = new();
        private IOldEvent _oldEvent;

        public EventRunner(
            IEventFactory eventFactory,
            IBattleSceneController battleSceneController)
        {
            _eventFactory = eventFactory;
            _battleSceneController = battleSceneController;
            _history.Push(EventCode.InitializationEvent);
            SetInput();
        }

        public void Run()
        {
            if (_history.Count == 0) throw new InvalidOperationException();
            var index = _history.Peek();
            if (index == WaitEvent) return;
            _oldEvent = _eventFactory.Create(index);
            var nextIndex = _oldEvent.Run();
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
            if (_oldEvent is not IWait waitEvent) return;
            var index = waitEvent.NextEvent();
            _history.Pop(); // WaitEventを削除
            _history.Push(index);
        }

        private void OnCancelAction()
        {
            if (_oldEvent is not ICancelable cancelableEvent) return;
            cancelableEvent.CancelAction();
            _history.Pop(); // WaitEventを削除
            _history.Pop(); // 自分自身を削除
        }

        private void OnSelectAction(Vector2 vector2)
        {
            if (_oldEvent is not ISelectable selectableEvent) return;
            selectableEvent.SelectAction(vector2);
        }
    }
}