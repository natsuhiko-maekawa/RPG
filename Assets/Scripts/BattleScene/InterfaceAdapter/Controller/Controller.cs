using BattleScene.InterfaceAdapter.IInputSystem;
using BattleScene.InterfaceAdapter.IView;
using VContainer.Unity;

namespace BattleScene.InterfaceAdapter.Controller
{
    public class Controller : IStartable
    {
        private readonly StateMachine _stateMachine;
        private readonly IBattleSceneInputSystem _battleSceneInputSystem;
        private readonly IGridView _gridView;

        public Controller(
            StateMachine stateMachine,
            IBattleSceneInputSystem battleSceneInputSystem,
            IGridView gridView)
        {
            _stateMachine = stateMachine;
            _battleSceneInputSystem = battleSceneInputSystem;
            _gridView = gridView;
        }

        void IStartable.Start()
        {
            _stateMachine.Start();
            _battleSceneInputSystem.SetOnNextAction(_stateMachine.Select);
            _gridView.SetSelectAction(x => _stateMachine.SelectAction(x));
        }
    }
}