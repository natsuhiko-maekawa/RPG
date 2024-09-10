using BattleScene.Domain.DomainService;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class OrderDecision
    {
        private readonly ActionTimeService _actionTime;
        private readonly CharactersDomainService _characters;
        private readonly OrderService _order;

        public OrderDecision(
            ActionTimeService actionTime,
            CharactersDomainService characters,
            OrderService order)
        {
            _actionTime = actionTime;
            _characters = characters;
            _order = order;
        }

        public void Execute()
        {
            _order.Update(_characters.GetIdList());
            _actionTime.Update(_characters.GetIdList());
        }
    }
}