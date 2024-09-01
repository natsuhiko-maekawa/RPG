using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.IInput;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.State.Battle;
using VContainer;
using VContainer.Unity;

namespace BattleScene.InterfaceAdapter
{
    public class StateMachine : IStartable
    {
        private Context _context;
        private readonly IBattleSceneInput _battleSceneInput;
        private readonly IGridView _gridView;
        private readonly IObjectResolver _container;

        public StateMachine(
            IBattleSceneInput battleSceneInput,
            IGridView gridView,
            IObjectResolver container)
        {
            _battleSceneInput = battleSceneInput;
            _gridView = gridView;
            _container = container;
        }

        void IStartable.Start()
        {
            _context = new Context(_container.Resolve<InitializeBattleState>());
            _battleSceneInput.SetSelectAction(_context.Select);
            _gridView.SetSelectAction(x => _context.Select((ActionCode)x));
        }

        public void Start()
        {
            _context = new Context(_container.Resolve<InitializeBattleState>());
        }
        
        public void Select()
        {
            _context.Select();
        }
        
        public void SelectAction(int actionCode)
        {
            _context.Select((ActionCode)actionCode);
        }
    }
}