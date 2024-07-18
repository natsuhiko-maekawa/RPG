using BattleScene.UseCases.StateMachine;
using UnityEngine;
using VContainer;

namespace BattleScene.UseCases
{
    internal class BattleScene : MonoBehaviour
    {
        private EventExecutor _eventExecutor;
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

        [Inject]
        public void Construct(
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
            Debug.Log(StateCode.ToString());
        }

        public void Update()
        {
            
        }
    }
}