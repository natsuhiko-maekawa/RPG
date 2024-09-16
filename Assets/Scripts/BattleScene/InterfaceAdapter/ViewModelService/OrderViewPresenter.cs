using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.DataAccess;
using EnemyViewDto = BattleScene.InterfaceAdapter.DataAccess.Dto.EnemyViewDto;
using static BattleScene.Domain.Code.SlipDamageCode;
using static BattleScene.Domain.Code.AilmentCode;

namespace BattleScene.InterfaceAdapter.ViewModelService
{
    internal class OrderViewPresenter
    {
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly Framework.View.OrderView _orderView;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<OrderedItemEntity, OrderId> _orderedItemRepository;

        public OrderViewPresenter(
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            OrderView orderView, 
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<OrderedItemEntity, OrderId> orderedItemRepository)
        {
            _enemyViewInfoResource = enemyViewInfoResource;
            _orderView = orderView;
            _characterRepository = characterRepository;
            _orderedItemRepository = orderedItemRepository;
        }

        public async void StartAnimationAsync()
        {
            var orderViewDtoList = _orderedItemRepository.Select()
                .OrderBy(x => x.OrderNumber)
                .Select(CreateOrderViewDto)
                .ToImmutableList();

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
            var dto = _characterRepository.Select(characterId).IsPlayer
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
            var characterTypeId = _characterRepository.Select(characterId).CharacterTypeCode;
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

        private int ConvertToIntFrom(SlipDamageCode slipDamageCode)
        {
            return slipDamageCode switch
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