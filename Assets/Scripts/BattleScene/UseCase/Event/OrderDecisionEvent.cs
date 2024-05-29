using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.OrderView.OutputBoundary;
using BattleScene.UseCase.View.OrderView.OutputDataFactory;
using Utility;
using static BattleScene.Domain.Code.AilmentCode;

namespace BattleScene.UseCase.Event
{
    internal class OrderDecisionEvent : IEvent
    {
        private readonly IActionTimeRepository _actionTimeRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IRandomEx _randomEx;
        private readonly AilmentDomainService _ailment;
        private readonly CharactersDomainService _characters;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ActionTimeCreatorService _actionTimeCreator;
        private readonly OrderedItemCreatorService _orderedItemCreator;
        private readonly OrderOutputDataFactory _outputDataFactory;
        private readonly IOrderViewPresenter _orderView;

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