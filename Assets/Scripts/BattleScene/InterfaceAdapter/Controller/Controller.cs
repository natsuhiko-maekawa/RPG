using BattleScene.InterfaceAdapter.IInput;
using BattleScene.InterfaceAdapter.IView;
using VContainer.Unity;

namespace BattleScene.InterfaceAdapter.Controller
{
    public class Controller : IStartable
    {
        private readonly StateMachine _stateMachine;
        private readonly IBattleSceneInput _battleSceneInput;
        private readonly IGridView _gridView;

        public Controller(
            StateMachine stateMachine,
            IBattleSceneInput battleSceneInput,
            IGridView gridView)
        {
            _stateMachine = stateMachine;
            _battleSceneInput = battleSceneInput;
            _gridView = gridView;
        }

        void IStartable.Start()
        {
            _stateMachine.Start();
            _battleSceneInput.SetSelectAction(_stateMachine.Select);
            _gridView.SetSelectAction(x => _stateMachine.SelectAction(x));
        }
    }
}