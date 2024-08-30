using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.UseCase.Interface;

namespace BattleScene.UseCases.UseCase
{
    internal class OrderDecision : IUseCase
    {
        private readonly ActionTimeCreatorService _actionTimeCreator;
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly CharactersDomainService _characters;
        private readonly OrderedItemCreatorService _orderedItemCreator;
        private readonly IRepository<OrderedItemEntity, OrderNumber> _orderedItemRepository;

        public OrderDecision(
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
            _orderedItemCreator.Create(_characters.GetIdList());

            var actionTimeList = _actionTimeCreator.Create(_characters.GetIdList());
            _actionTimeRepository.Update(actionTimeList);
        }
    }
}