using System;
using System.Collections.Generic;
using R3;

namespace BattleScene.InterfaceAdapter
{
    public class AnimationQueue : IDisposable
    {
        private Queue<Action>? _queue;
        public ReactiveCommand OnLastAnimate { get; } = new();

        public AnimationQueue(Queue<Action> queue)
        {
            if (queue.Count == 0)
                throw new ArgumentException("Argument queue size must be bigger than 1 but it was 0.");
            _queue = queue;
        }

        public void Animate()
        {
            if (_queue!.Count == 0)
            {
                OnLastAnimate.Execute(Unit.Default);
                return;
            }

            var action = _queue.Dequeue();
            action.Invoke();
        }

        public void Dispose()
        {
            _queue = null;
            OnLastAnimate.Dispose();
        }
    }
}