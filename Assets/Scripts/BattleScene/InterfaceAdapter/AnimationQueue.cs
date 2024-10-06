using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;

namespace BattleScene.InterfaceAdapter
{
    public class AnimationQueue
    {
        private readonly Queue<Task> _taskQueue;
        
        public event Action OnLastAnimate;

        public AnimationQueue(IList<Task> tasks)
        {
            if (tasks.Count == 0) throw new ArgumentException("Argument tasks size must be bigger than 1 but it was 0.");
            _taskQueue = new Queue<Task>(tasks);
        }

        public async Task Animate()
        {
            if (_taskQueue.Count == 0)
            {
                MyDebug.LogAssertion("Executed Animate method but task queue is empty.");
                return;
            }
            var task = _taskQueue.Dequeue();
            if (_taskQueue.Count == 0) OnLastAnimate?.Invoke();
            await task;
        }
    }
}