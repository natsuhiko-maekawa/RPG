using BattleScene.UseCases.Services;
using BattleScene.UseCases.Services.Order;

namespace BattleScene.UseCases.UseCases
{
    public class InitializeBattleUseCase
    {
        private readonly OrderService _order;
        private readonly TurnService _turn;

        public InitializeBattleUseCase(
            OrderService order,
            TurnService turn)
        {
            _order = order;
            _turn = turn;
        }

        public void Initialize()
        {
            _turn.Initialize();
            _order.Initialize();
        }
    }
}