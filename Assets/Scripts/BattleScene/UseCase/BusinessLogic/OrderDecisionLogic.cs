using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.BusinessLogic.Interface;
using BattleScene.UseCase.Service;
using Utility.Interface;
using static BattleScene.Domain.Code.AilmentCode;

namespace BattleScene.UseCase.BusinessLogic
{
    internal class OrderDecisionLogic : IUseCase
    {
        private readonly ActionTimeCreatorService _actionTimeCreator;
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly AilmentDomainService _ailment;
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly OrderedItemCreatorService _orderedItemCreator;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRepository<OrderedItemEntity, OrderNumber> _orderedItemRepository;
        private readonly IRandomEx _randomEx;

        public OrderDecisionLogic(
            ActionTimeCreatorService actionTimeCreator,
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            AilmentDomainService ailment,
            ICharacterRepository characterRepository,
            CharactersDomainService characters,
            OrderedItemCreatorService orderedItemCreator,
            OrderedItemsDomainService orderedItems,
            IRepository<OrderedItemEntity, OrderNumber> orderedItemRepository,
            IRandomEx randomEx)
        {
            _actionTimeCreator = actionTimeCreator;
            _actionTimeRepository = actionTimeRepository;
            _ailment = ailment;
            _characterRepository = characterRepository;
            _characters = characters;
            _orderedItemCreator = orderedItemCreator;
            _orderedItems = orderedItems;
            _orderedItemRepository = orderedItemRepository;
            _randomEx = randomEx;
        }

        public void Execute()
        {
            var order = _orderedItemCreator.Create(_characters.GetIdList());
            _orderedItemRepository.Update(order);

            var actionTimeList = _actionTimeCreator.Create(_characters.GetIdList());
            _actionTimeRepository.Update(actionTimeList);

            var businessLogicCode = _orderedItems.FirstItem() switch
            {
                OrderedCharacterValueObject => DecideNextEvent(_orderedItems.FirstCharacterId()),
                OrderedAilmentValueObject => UseCaseCode.AilmentResetLogic,
                OrderedSlipDamageValueObject => UseCaseCode.AilmentsSlipMessageLogic,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private UseCaseCode DecideNextEvent(CharacterId characterId)
        {
            var ailmentCode = _ailment.GetHighPriority(characterId)?.AilmentCode;

            switch (ailmentCode)
            {
                case Sleep:
                case Stun:
                case Petrifaction:
                    return UseCaseCode.CantActionLogic;
                case Paralysis:
                case EnemyParalysis:
                    if (_randomEx.Probability(0.5f))
                        return UseCaseCode.CantActionLogic;
                    break;
                case Confusion:
                    return _characterRepository.Select(characterId).IsPlayer()
                        ? UseCaseCode.PlayerConfusionLogic
                        : UseCaseCode.EnemyConfusionLogic;
            }

            return _characterRepository.Select(characterId).IsPlayer()
                ? UseCaseCode.SelectActionLogic
                : UseCaseCode.EnemySelectSkillLogic;
        }
    }
}