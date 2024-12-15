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

namespace BattleScene.Presenters.Presenters
{
    public class OrderViewPresenter
    {
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly OrderView _orderView;
        private readonly IRepository<OrderItemEntity, OrderItemId> _orderItemRepository;

        public OrderViewPresenter(
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            OrderView orderView,
            IRepository<OrderItemEntity, OrderItemId> orderItemRepository)
        {
            _enemyViewInfoResource = enemyViewInfoResource;
            _orderView = orderView;
            _orderItemRepository = orderItemRepository;
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
            var orderViewDtoList = _orderItemRepository.Get()
                .OrderBy(x => x.Order)
                .Select(CreateOrderViewDto)
                .ToArray();
            _orderView.StartAnimation(orderViewDtoList);
        }

        private OrderViewModel CreateOrderViewDto(OrderItemEntity orderItem)
        {
            var dto = orderItem.ActorType switch
            {
                ActorType.Actor => CreateCharacterOrderViewDto(orderItem),
                ActorType.Ailment => CreateAilmentOrderViewDto(orderItem),
                ActorType.Slip => CreateSlipOrderViewDto(orderItem),
                _ => throw new ArgumentOutOfRangeException()
            };

            return dto;
        }

        private OrderViewModel CreateCharacterOrderViewDto(OrderItemEntity orderItem)
        {
            if (!orderItem.TryGetActor(out var actor))
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

        private OrderViewModel CreateAilmentOrderViewDto(OrderItemEntity orderItem)
        {
            orderItem.TryGetAilmentCode(out var ailmentCode);
            var ailmentInt = ConvertToIntFrom(ailmentCode);
            var dto = new OrderViewModel(ItemType.Ailment, AilmentInt: ailmentInt);
            return dto;
        }

        private OrderViewModel CreateSlipOrderViewDto(OrderItemEntity orderItem)
        {
            orderItem.TryGetSlipCode(out var slipCode);
            var ailmentInt = ConvertToIntFrom(slipCode);
            var dto = new OrderViewModel(ItemType.Ailment, AilmentInt: ailmentInt);
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