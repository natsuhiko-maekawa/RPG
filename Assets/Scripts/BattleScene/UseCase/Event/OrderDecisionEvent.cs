using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.OrderView.OutputBoundary;
using BattleScene.UseCase.View.OrderView.OutputDataFactory;
using Utility;
using static BattleScene.Domain.Code.AilmentCode;

namespace BattleScene.UseCase.Event
{
    internal class OrderDecisionEvent : IEvent
    {
        private readonly ActionTimeCreatorService _actionTimeCreator;
        private readonly IActionTimeRepository _actionTimeRepository;
        private readonly AilmentDomainService _ailment;
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly OrderedItemCreatorService _orderedItemCreator;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderViewPresenter _orderView;
        private readonly OrderOutputDataFactory _outputDataFactory;
        private readonly IRandomEx _randomEx;

        public OrderDecisionEvent(
            ActionTimeCreatorService actionTimeCreator,
            IActionTimeRepository actionTimeRepository,
            AilmentDomainService ailment,
            ICharacterRepository characterRepository,
            CharactersDomainService characters,
            OrderedItemCreatorService orderedItemCreator,
            OrderedItemsDomainService orderedItems,
            IOrderRepository orderRepository,
            IOrderViewPresenter orderView,
            OrderOutputDataFactory outputDataFactory,
            IRandomEx randomEx)
        {
            _actionTimeCreator = actionTimeCreator;
            _actionTimeRepository = actionTimeRepository;
            _ailment = ailment;
            _characterRepository = characterRepository;
            _characters = characters;
            _orderedItemCreator = orderedItemCreator;
            _orderedItems = orderedItems;
            _orderRepository = orderRepository;
            _orderView = orderView;
            _outputDataFactory = outputDataFactory;
            _randomEx = randomEx;
        }

        public EventCode Run()
        {
            var order = _orderedItemCreator.Create(_characters.GetIdList());
            _orderRepository.Update(order);

            var actionTimeList = _actionTimeCreator.Create(_characters.GetIdList());
            _actionTimeRepository.Update(actionTimeList);
            var orderOutputDataList = _outputDataFactory.Create();
            _orderView.Start(orderOutputDataList);

            return _orderedItems.FirstItem() switch
            {
                OrderedCharacterValueObject => DecideNextEvent(_orderedItems.FirstCharacterId()),
                OrderedAilmentValueObject => EventCode.AilmentResetEvent,
                OrderedSlipDamageValueObject => EventCode.AilmentsSlipMessageEvent,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private EventCode DecideNextEvent(CharacterId characterId)
        {
            var ailmentCode = _ailment.GetHighPriority(characterId).AilmentCode;
            {
                switch (ailmentCode)
                {
                    case Sleep:
                    case Stun:
                    case Petrifaction:
                        return EventCode.CantActionEvent;
                    case Paralysis:
                    case EnemyParalysis:
                        if (_randomEx.Probability(0.5f))
                            return EventCode.CantActionEvent;
                        break;
                    case Confusion:
                        return _characterRepository.Select(characterId).IsPlayer()
                            ? EventCode.PlayerConfusionEvent
                            : EventCode.EnemyConfusionEvent;
                }
            }

            return _characterRepository.Select(characterId).IsPlayer()
                ? EventCode.SelectActionEvent
                : EventCode.EnemySelectSkillEvent;
        }
    }
}