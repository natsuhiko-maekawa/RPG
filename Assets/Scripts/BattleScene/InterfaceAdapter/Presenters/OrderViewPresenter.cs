using System;
using System.Linq;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;
using static BattleScene.Domain.Codes.SlipCode;
using static BattleScene.Domain.Codes.AilmentCode;

namespace BattleScene.InterfaceAdapter.Presenters
{
    public class OrderViewPresenter
    {
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly OrderView _orderView;
        private readonly IRepository<OrderedItemEntity, OrderedItemId> _orderedItemRepository;

        public OrderViewPresenter(
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            OrderView orderView,
            IRepository<OrderedItemEntity, OrderedItemId> orderedItemRepository)
        {
            _enemyViewInfoResource = enemyViewInfoResource;
            _orderView = orderView;
            _orderedItemRepository = orderedItemRepository;
        }

        public void Initialize(CharacterEntity[] characterArray)
        {
            var enemyImagePathArray = characterArray
                .Select(x => x.CharacterTypeCode)
                .Distinct()
                .Select(x => _enemyViewInfoResource.Get(x).ImagePath)
                .ToArray();
            _orderView.Initialize(enemyImagePathArray);
        }

        public void StartAnimation()
        {
            var orderViewDtoList = _orderedItemRepository.Get()
                .OrderBy(x => x.Order)
                .Select(CreateOrderViewDto)
                .ToArray();
            _orderView.StartAnimation(orderViewDtoList);
        }

        private OrderViewModel CreateOrderViewDto(OrderedItemEntity orderedItem)
        {
            var dto = orderedItem.ActorType switch
            {
                ActorType.Actor => CreateCharacterOrderViewDto(orderedItem),
                ActorType.Ailment => CreateAilmentOrderViewDto(orderedItem),
                ActorType.Slip => CreateSlipOrderViewDto(orderedItem),
                _ => throw new ArgumentOutOfRangeException()
            };

            return dto;
        }

        private OrderViewModel CreateCharacterOrderViewDto(OrderedItemEntity orderedItem)
        {
            if (!orderedItem.TryGetActor(out var actor))
                throw new InvalidOperationException();
            var dto = actor.IsPlayer
                ? CreatePlayerOrderViewDto()
                : CreateEnemyOrderViewDto(actor);
            return dto;
        }

        private OrderViewModel CreatePlayerOrderViewDto()
        {
            var playerOrderViewDto = new OrderViewModel(
                ItemType: ItemType.Player);
            return playerOrderViewDto;
        }

        private OrderViewModel CreateEnemyOrderViewDto(CharacterEntity actor)
        {
            var characterTypeId = actor.CharacterTypeCode;
            var enemyImagePath = _enemyViewInfoResource.Get(characterTypeId).ImagePath;
            return new OrderViewModel(ItemType.Enemy, EnemyImagePath: enemyImagePath);
        }

        private OrderViewModel CreateAilmentOrderViewDto(OrderedItemEntity orderedItem)
        {
            orderedItem.TryGetAilmentCode(out var ailmentCode);
            var ailmentNumber = ConvertToIntFrom(ailmentCode);
            var dto = new OrderViewModel(ItemType.Ailment, AilmentNumber: ailmentNumber);
            return dto;
        }

        private OrderViewModel CreateSlipOrderViewDto(OrderedItemEntity orderedItem)
        {
            orderedItem.TryGetSlipCode(out var slipCode);
            var ailmentNumber = ConvertToIntFrom(slipCode);
            var dto = new OrderViewModel(ItemType.Ailment, AilmentNumber: ailmentNumber);
            return dto;
        }

        private int ConvertToIntFrom(AilmentCode ailmentCode)
        {
            return ailmentCode switch
            {
                Blind => 0,
                Deaf => 1,
                Confusion => 2,
                Paralysis => 3,
                Sleep => 4,
                Stun => 5,
                Petrifaction => 6,
                Curse => 13,
                Binding => 14,
                EnemyBlind => 0,
                EnemyDeaf => 1,
                EnemyParalysis => 3,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private int ConvertToIntFrom(SlipCode slipCode)
        {
            return slipCode switch
            {
                Burning => 7,
                Freeze => 8,
                ElectricShock => 9,
                Poisoning => 10,
                Bleeding => 11,
                Suffocation => 12,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}