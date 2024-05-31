using BattleScene.UseCase.Event.Runner;
using UnityEngine;
using VContainer;

namespace BattleScene.UseCase
{
    internal class BattleScene : MonoBehaviour
    {
        private EventRunner _eventRunner;

        private void Update()
        {
            _eventRunner.Run();
        }

        [Inject]
        public void Construct(EventRunner eventRunner)
        {
            _eventRunner = eventRunner;
        }
    }
}