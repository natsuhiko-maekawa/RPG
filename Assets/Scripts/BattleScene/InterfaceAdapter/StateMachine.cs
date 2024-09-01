using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.State.Battle;
using VContainer;
using VContainer.Unity;

namespace BattleScene.InterfaceAdapter
{
    public class StateMachine　: IInitializable
    {
        private Context _context;
        private readonly IObjectResolver _container;

        public StateMachine(
            IObjectResolver container)
        {
            _container = container;
        }

        void IInitializable.Initialize()
        {
            _context = new Context(_container.Resolve<InitializeBattleState>());
        }
        
        public void Start()
        {
            _context = new Context(_container.Resolve<InitializeBattleState>());
        }

        public void Select()
        {
            _context.Select();
        }
        
        public void Select(ActionCode actionCode)
        {
            _context.Select(actionCode);
        }
    }
}