using System;
using System.Collections.Generic;
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
    internal class OrderDecisionLogic : IBusinessLogic
    {
        private readonly ActionTimeCreatorService _actionTimeCreator;
        private readonly IActionTimeRepository _actionTimeRepository;
        private readonly AilmentDomainService _ailment;
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly IFrameRepository _frameRepository;
        private readonly OrderedItemCreatorService _orderedItemCreator;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IOrderRepository _orderRepository;
        private readonly IRandomEx _randomEx;

        public OrderDecisionLogic(
            ActionTimeCreatorService actionTimeCreator,
            IActionTimeRepository actionTimeRepository,
            AilmentDomainService ailment,
            ICharacterRepository characterRepository,
            CharactersDomainService characters,
            IFrameRepository frameRepository,
            OrderedItemCreatorService orderedItemCreator,
            OrderedItemsDomainService orderedItems,
            IOrderRepository orderRepository,
            IRandomEx randomEx)
        {
            _actionTimeCreator = actionTimeCreator;
            _actionTimeRepository = actionTimeRepository;
            _ailment = ailment;
            _characterRepository = characterRepository;
            _characters = characters;
            _frameRepository = frameRepository;
            _orderedItemCreator = orderedItemCreator;
            _orderedItems = orderedItems;
            _orderRepository = orderRepository;
            _randomEx = randomEx;
        }

        public void Execute(FrameNumber nextFrameNumber)
        {
            var order = _orderedItemCreator.Create(_characters.GetIdList());
            _orderRepository.Update(order);

            var actionTimeList = _actionTimeCreator.Create(_characters.GetIdList());
            _actionTimeRepository.Update(actionTimeList);

            var businessLogicCode = _orderedItems.FirstItem() switch
            {
                OrderedCharacterValueObject => DecideNextEvent(_orderedItems.FirstCharacterId()),
                OrderedAilmentValueObject => BusinessLogicCode.AilmentResetLogic,
                OrderedSlipDamageValueObject => BusinessLogicCode.AilmentsSlipMessageLogic,
                _ => throw new ArgumentOutOfRangeException()
            };

            var frame = _frameRepository.Select(nextFrameNumber)
                .Merge(new FrameEntity(
                    frameNumber: nextFrameNumber,
                    businessLogicCodeList: new List<BusinessLogicCode> { businessLogicCode },
                    presenterCodeList: new List<PresenterCode> { PresenterCode.Order }));
            _frameRepository.Update(frame);
        }

        private BusinessLogicCode DecideNextEvent(CharacterId characterId)
        {
            var ailmentCode = _ailment.GetHighPriority(characterId)?.AilmentCode;

            switch (ailmentCode)
            {
                case Sleep:
                case Stun:
                case Petrifaction:
                    return BusinessLogicCode.CantActionLogic;
                case Paralysis:
                case EnemyParalysis:
                    if (_randomEx.Probability(0.5f))
                        return BusinessLogicCode.CantActionLogic;
                    break;
                case Confusion:
                    return _characterRepository.Select(characterId).IsPlayer()
                        ? BusinessLogicCode.PlayerConfusionLogic
                        : BusinessLogicCode.EnemyConfusionLogic;
            }

            return _characterRepository.Select(characterId).IsPlayer()
                ? BusinessLogicCode.SelectActionLogic
                : BusinessLogicCode.EnemySelectSkillLogic;
        }
    }
}