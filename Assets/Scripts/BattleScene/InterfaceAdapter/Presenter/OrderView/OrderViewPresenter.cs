using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.InterfaceAdapter.Presenter.OrderView
{
    internal class OrderViewPresenter : IOrderViewPresenter
    {
        private readonly IResource<EnemyViewInfoDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly IOrderView _orderView;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly ToAilmentNumberService _toAilmentNumber;

        public OrderViewPresenter(
            IResource<EnemyViewInfoDto, CharacterTypeCode> enemyViewInfoResource,
            IOrderView orderView,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
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
                        return _characterRepository.Select(characterId).IsPlayer()
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

            _orderView.StartAnimation(orderViewDtoList);
        }

        private OrderViewDto CreateEnemyViewDto(CharacterId characterId)
        {
            var characterTypeId = _characterRepository.Select(characterId).Property.CharacterTypeCode;
            var enemyImagePath = _enemyViewInfoResource.Get(characterTypeId).EnemyImagePath;
            return new OrderViewDto(ItemType.Enemy, EnemyImagePath: enemyImagePath);
        }
    }
}