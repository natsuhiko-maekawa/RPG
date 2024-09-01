using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.State.Battle;
using VContainer;

namespace BattleScene.InterfaceAdapter
{
    public class StateMachine
    {
        private Context _context;
        private readonly Controller.Controller _controller;
        private readonly IObjectResolver _container;

        public StateMachine(
            Controller.Controller controller,
            IObjectResolver container)
        {
            _controller = controller;
            _container = container;
            _controller.Subscribe(this);
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