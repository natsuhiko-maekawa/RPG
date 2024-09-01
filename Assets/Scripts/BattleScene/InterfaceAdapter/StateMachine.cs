using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.State.Battle;
using VContainer;

namespace BattleScene.InterfaceAdapter
{
    public class StateMachine
    {
        private Context _context;
        private readonly IObjectResolver _container;

        public StateMachine(
            IObjectResolver container)
        {
            _container = container;
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