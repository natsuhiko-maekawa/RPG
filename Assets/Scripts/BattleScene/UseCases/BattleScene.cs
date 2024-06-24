using UnityEngine;
using VContainer;

namespace BattleScene.UseCases
{
    internal class BattleScene : MonoBehaviour
    {
        private StateMachine.StateMachine _stateMachine;

        private void Start()
        {
            _stateMachine.Start();
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        [Inject]
        public void Construct(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}