using BattleScene.UseCases.StateMachine;
using UnityEngine;

namespace BattleScene.UseCases
{
    internal class BattleScene : MonoBehaviour
    {
        private readonly EventExecutor _eventExecutor;
        private StateCode _stateCode;
        private StateCode StateCode
        {
            get => _stateCode;
            set
            {
                _stateCode = value;
                OnChangeState();
            }
        }

        public BattleScene(
            EventExecutor eventExecutor)
        {
            _eventExecutor = eventExecutor;
        }

        public void Start()
        {
            StateCode = StateCode.Initialize;
        }

        public void OnSelect()
        {
            
        }

        public void OnRight()
        {
            
        }

        private void OnChangeState()
        {
            var stateCode = _eventExecutor.Execute(StateCode);
            StateCode = stateCode;
        }

        public void Update()
        {
            
        }
    }
}