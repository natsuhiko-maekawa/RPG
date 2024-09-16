using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.IPresenter;
using EnemyViewDto = BattleScene.InterfaceAdapter.DataAccess.Dto.EnemyViewDto;

namespace BattleScene.InterfaceAdapter.Presenter
{
    [Obsolete]
    internal class OrderViewPresenter : IOrderViewPresenter
    {
        private readonly IResource<EnemyViewDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly Framework.View.OrderView _orderView;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly ToAilmentNumberService _toAilmentNumber;

        public OrderViewPresenter(
            IResource<EnemyViewDto, CharacterTypeCode> enemyViewInfoResource,
            Framework.View.OrderView orderView,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            ToAilmentNumberService toAilmentNumber)
        {
            _enemyViewInfoResource = enemyViewInfoResource;
            _orderView = orderView;
            _characterRepository = characterRepository;
            _toAilmentNumber = toAilmentNumber;
        }

        public void Start(IList<OrderedItemEntity> order)
        {
            var orderViewDtoList = order
                .Select(x =>
                {
                    if (x.TryGetCharacterId(out var characterId))
                    {
                        return _characterRepository.Select(characterId).IsPlayer
                            ? new OrderViewDto(ItemType.Player)
                            : CreateEnemyViewDto(characterId);
                    }

                    if (x.TryGetAilmentCode(out var ailmentCode))
                    {
                        var ailmentNumber = _toAilmentNumber.Ailment(ailmentCode);
                        return new OrderViewDto(ItemType.Ailment, AilmentNumber: ailmentNumber);
                    }

                    if (x.TryGetSlipDamageCode(out var slipDamageCode))
                    {
                        var ailmentNumber = _toAilmentNumber.SlipDamage(slipDamageCode);
                        return new OrderViewDto(ItemType.Ailment, AilmentNumber: ailmentNumber);
                    }

                    throw new InvalidOperationException();
                })
                .ToImmutableList();

            _orderView.StartAnimationAsync(orderViewDtoList);
        }

        private OrderViewDto CreateEnemyViewDto(CharacterId characterId)
        {
            var characterTypeId = _characterRepository.Select(characterId).CharacterTypeCode;
            var enemyImagePath = _enemyViewInfoResource.Get(characterTypeId).EnemyImagePath;
            return new OrderViewDto(ItemType.Enemy, EnemyImagePath: enemyImagePath);
        }
    }
}