using BattleScene.UseCase.Main;
using UnityEngine;
using VContainer;

namespace BattleScene.UseCase
{
    internal class BattleScene : MonoBehaviour
    {
        private StateMachine _stateMachine;

        private void Start()
        {
            _stateMachine.Start();
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        [Inject]
        public void Construct(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}