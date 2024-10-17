using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattleScene.InterfaceAdapter
{
    public class AnimationQueue
    {
        private readonly Queue<Func<Task>> _taskQueue;

        public event Action? OnLastAnimate;

        public AnimationQueue(Queue<Func<Task>> tasks)
        {
            if (tasks.Count == 0)
                throw new ArgumentException("Argument tasks size must be bigger than 1 but it was 0.");
            _taskQueue = new Queue<Func<Task>>(tasks);
        }

        public async Task Animate()
        {
            if (_taskQueue.Count == 0)
            {
                OnLastAnimate?.Invoke();
                return;
            }

            var task = _taskQueue.Dequeue();
            await task.Invoke();
        }
    }
}