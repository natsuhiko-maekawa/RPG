using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class OrderViewPresenter
    {
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly OrderView _orderView;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly ICollection<OrderedItemEntity, OrderId> _orderedItemCollection;

        public OrderViewPresenter(
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            OrderView orderView,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            ICollection<OrderedItemEntity, OrderId> orderedItemCollection)
        {
            _enemyViewInfoResource = enemyViewInfoResource;
            _orderView = orderView;
            _characterCollection = characterCollection;
            _orderedItemCollection = orderedItemCollection;
        }

        public async void StartAnimationAsync()
        {
            var orderViewDtoList = _orderedItemCollection.Get()
                .OrderBy(x => x.OrderNumber)
                .Select(CreateOrderViewDto)
                .ToList();

            await _orderView.StartAnimationAsync(orderViewDtoList);
        }

        private OrderViewDto CreateOrderViewDto(OrderedItemEntity orderedItem)
        {
            var dto = orderedItem.OrderedItemType switch
            {
                OrderedItemType.Character => CreateCharacterOrderViewDto(orderedItem),
                OrderedItemType.Ailment => CreateAilmentOrderViewDto(orderedItem),
                OrderedItemType.Slip => CreateSlipOrderViewDto(orderedItem),
                _ => throw new ArgumentOutOfRangeException()
            };

            return dto;
        }

        private OrderViewDto CreateCharacterOrderViewDto(OrderedItemEntity orderedItem)
        {
            orderedItem.TryGetCharacterId(out var characterId);
            var dto = _characterCollection.Get(characterId).IsPlayer
                ? CreatePlayerOrderViewDto()
                : CreateEnemyOrderViewDto(characterId);
            return dto;
        }

        private OrderViewDto CreatePlayerOrderViewDto()
        {
            var playerOrderViewDto = new OrderViewDto(
                ItemType: ItemType.Player);
            return playerOrderViewDto;
        }

        private OrderViewDto CreateEnemyOrderViewDto(CharacterId characterId)
        {
            var characterTypeId = _characterCollection.Get(characterId).CharacterTypeCode;
            var enemyImagePath = _enemyViewInfoResource.Get(characterTypeId).EnemyImagePath;
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
            orderedItem.TryGetSlipDamageCode(out var slipCode);
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