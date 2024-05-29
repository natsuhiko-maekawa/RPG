using UnityEngine;
using VContainer;

namespace BattleScene.UseCase
{
    internal class BattleScene : MonoBehaviour
    {
        private EventRunner.EventRunner _eventRunner;
        
        [Inject]
        public void Construct(EventRunner.EventRunner eventRunner)
        {
            _eventRunner = eventRunner;
        }
        
        private void Update()
        {
            _eventRunner.Run();
        }
    }
}