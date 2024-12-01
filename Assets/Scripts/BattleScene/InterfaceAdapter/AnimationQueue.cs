using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattleScene.InterfaceAdapter
{
    public class AnimationQueue
    {
        private readonly Queue<Action> _queue;

        public event Action? OnLastAnimate;

        public AnimationQueue(Queue<Action> tasks)
        {
            if (tasks.Count == 0)
                throw new ArgumentException("Argument tasks size must be bigger than 1 but it was 0.");
            _queue = new Queue<Action>(tasks);
        }

        public void Animate()
        {
            if (_queue.Count == 0)
            {
                OnLastAnimate?.Invoke();
                return;
            }

            var action = _queue.Dequeue();
            action.Invoke();
        }
    }
}