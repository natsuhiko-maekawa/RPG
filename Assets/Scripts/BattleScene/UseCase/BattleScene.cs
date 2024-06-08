using BattleScene.UseCase.Main;
using UnityEngine;
using VContainer;

namespace BattleScene.UseCase
{
    internal class BattleScene : MonoBehaviour
    {
        // private EventRunner _eventRunner;
        //
        // private void Update()
        // {
        //     _eventRunner.Run();
        // }
        //
        // [Inject]
        // public void Construct(EventRunner eventRunner)
        // {
        //     _eventRunner = eventRunner;
        // }
        
        private Runner _runner;

        private void Start()
        {
            _runner.Start();
        }

        private void Update()
        {
            _runner.Update();
        }

        [Inject]
        public void Construct(Runner runner)
        {
            _runner = runner;
        }
    }
}