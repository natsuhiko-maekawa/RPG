using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.BusinessLogic.Interface;
using BattleScene.UseCase.Service;

namespace BattleScene.UseCase.BusinessLogic
{
    internal class OrderDecisionLogic : IUseCase
    {
        private readonly ActionTimeCreatorService _actionTimeCreator;
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly CharactersDomainService _characters;
        private readonly OrderedItemCreatorService _orderedItemCreator;
        private readonly IRepository<OrderedItemEntity, OrderNumber> _orderedItemRepository;

        public OrderDecisionLogic(
            ActionTimeCreatorService actionTimeCreator,
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            CharactersDomainService characters,
            OrderedItemCreatorService orderedItemCreator,
            IRepository<OrderedItemEntity, OrderNumber> orderedItemRepository)
        {
            _actionTimeCreator = actionTimeCreator;
            _actionTimeRepository = actionTimeRepository;
            _characters = characters;
            _orderedItemCreator = orderedItemCreator;
            _orderedItemRepository = orderedItemRepository;
        }

        public void Execute()
        {
            var order = _orderedItemCreator.Create(_characters.GetIdList());
            _orderedItemRepository.Update(order);

            var actionTimeList = _actionTimeCreator.Create(_characters.GetIdList());
            _actionTimeRepository.Update(actionTimeList);
        }
    }
}