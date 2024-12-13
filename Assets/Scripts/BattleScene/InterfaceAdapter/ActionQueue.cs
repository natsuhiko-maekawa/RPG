using System;
using System.Collections.Generic;
using R3;

namespace BattleScene.InterfaceAdapter
{
    public struct ActionQueue : IDisposable
    {
        private Queue<Action>? _queue;
        public ReactiveCommand OnLastAction { get; }

        public ActionQueue(Queue<Action> queue)
        {
            if (queue.Count == 0)
                throw new ArgumentException("Argument queue size must be bigger than 1 but it was 0.");
            _queue = queue;
            OnLastAction = new ReactiveCommand();
        }

        public void Invoke()
        {
            if (_queue!.Count == 0)
            {
                OnLastAction.Execute(Unit.Default);
                return;
            }

            var action = _queue.Dequeue();
            action.Invoke();
        }

        public void Dispose()
        {
            _queue = null;
            OnLastAction.Dispose();
        }
    }
}