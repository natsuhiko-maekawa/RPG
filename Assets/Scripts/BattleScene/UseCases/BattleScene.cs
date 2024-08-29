using BattleScene.UseCases.IController;
using BattleScene.UseCases.StateMachine;
using UnityEngine;
using VContainer;

namespace BattleScene.UseCases
{
    internal class BattleScene : MonoBehaviour
    {
        private Context _context;
        private IObjectResolver _container;
        private IBattleSceneController _battleSceneController;

        [Inject]
        public void Construct(
            IObjectResolver container,
            IBattleSceneController battleSceneController)
        {
            _container = container;
            _battleSceneController = battleSceneController;
        }

        public void Start()
        {
            _context = new Context(_container.Resolve<InitializationState>());
            _battleSceneController.SetOnNextAction(_context.Select);
        }

        public void Update()
        {
            
        }
    }
}