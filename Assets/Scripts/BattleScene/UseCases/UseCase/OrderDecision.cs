using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.UseCase.Interface;

namespace BattleScene.UseCases.UseCase
{
    public class OrderDecision : IUseCase
    {
        private readonly ActionTimeCreatorService _actionTimeCreator;
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly CharactersDomainService _characters;
        private readonly OrderedItemCreatorService _orderedItemCreator;

        public OrderDecision(
            ActionTimeCreatorService actionTimeCreator,
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            CharactersDomainService characters,
            OrderedItemCreatorService orderedItemCreator)
        {
            _actionTimeCreator = actionTimeCreator;
            _actionTimeRepository = actionTimeRepository;
            _characters = characters;
            _orderedItemCreator = orderedItemCreator;
        }

        public void Execute()
        {
            _orderedItemCreator.Create(_characters.GetIdList());

            var actionTimeList = _actionTimeCreator.Create(_characters.GetIdList());
            _actionTimeRepository.Update(actionTimeList);
        }
    }
}