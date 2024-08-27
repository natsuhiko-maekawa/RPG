using BattleScene.UseCases.StateMachine;
using UnityEngine;
using VContainer;

namespace BattleScene.UseCases
{
    internal class BattleScene : MonoBehaviour
    {
        private Context _context;
        private IObjectResolver _container;

        [Inject]
        public void Construct(
            IObjectResolver container)
        {
            _container = container;
        }

        public void Start()
        {
            _context = new Context(_container.Resolve<InitializationState>());
        }

        public void Update()
        {
            
        }
    }
}