using System;
using System.Linq;
using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using EnemyViewDto = BattleScene.DataAccess.Dto.EnemyViewDto;
using static BattleScene.Domain.Code.SlipCode;
using static BattleScene.Domain.Code.AilmentCode;

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

        public async void StartAnimationAsync()
        {
            var orderViewDtoList = _orderedItemRepository.Get()
                .OrderBy(x => x.Order)
                .Select(CreateOrderViewDto)
                .ToList();

            await _orderView.StartAnimationAsync(orderViewDtoList);
        }

        private OrderViewDto CreateOrderViewDto(OrderedItemEntity orderedItem)
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

        private OrderViewDto CreateCharacterOrderViewDto(OrderedItemEntity orderedItem)
        {
            if (!orderedItem.TryGetActor(out var actor))
                throw new InvalidOperationException();
            var dto = actor.IsPlayer
                ? CreatePlayerOrderViewDto()
                : CreateEnemyOrderViewDto(actor);
            return dto;
        }

        private OrderViewDto CreatePlayerOrderViewDto()
        {
            var playerOrderViewDto = new OrderViewDto(
                ItemType: ItemType.Player);
            return playerOrderViewDto;
        }

        private OrderViewDto CreateEnemyOrderViewDto(CharacterEntity actor)
        {
            var characterTypeId = actor.CharacterTypeCode;
            var enemyImagePath = _enemyViewInfoResource.Get(characterTypeId).ImagePath;
            return new OrderViewDto(ItemType.Enemy, EnemyImagePath: enemyImagePath);
        }

        private OrderViewDto CreateAilmentOrderViewDto(OrderedItemEntity orderedItem)
        {
            orderedItem.TryGetAilmentCode(out var ailmentCode);
            var ailmentNumber = ConvertToIntFrom(ailmentCode);
            var dto = new OrderViewDto(ItemType.Ailment, AilmentNumber: ailmentNumber);
            return dto;
        }

        private OrderViewDto CreateSlipOrderViewDto(OrderedItemEntity orderedItem)
        {
            orderedItem.TryGetSlipCode(out var slipCode);
            var ailmentNumber = ConvertToIntFrom(slipCode);
            var dto = new OrderViewDto(ItemType.Ailment, AilmentNumber: ailmentNumber);
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