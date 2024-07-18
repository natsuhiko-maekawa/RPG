using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.View.OrderView.OutputBoundary;
using BattleScene.UseCases.View.OrderView.OutputData;
using static BattleScene.UseCases.View.OrderView.OutputData.OrderOutputDataType;

namespace BattleScene.InterfaceAdapter.Presenter.OrderView
{
    internal class OrderViewPresenter : IOrderViewPresenter
    {
        private readonly IEnemyViewInfoFactory _enemyViewInfoFactory;
        private readonly IOrderView _orderView;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly ToAilmentNumberService _toAilmentNumber;

        public OrderViewPresenter(
            IEnemyViewInfoFactory enemyViewInfoFactory,
            IOrderView orderView,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            ToAilmentNumberService toAilmentNumber)
        {
            _enemyViewInfoFactory = enemyViewInfoFactory;
            _orderView = orderView;
            _characterRepository = characterRepository;
            _toAilmentNumber = toAilmentNumber;
        }

        public void Start(IList<OrderOutputData> orderOutputDataList)
        {
            var orderViewDtoList = orderOutputDataList
                .Select(x =>
                {
                    return x.OrderOutputDataType switch
                    {
                        Player => CreatePlayer(),
                        Enemy => CreateEnemy(x.CharacterTypeId),
                        Ailment => CreateAilment(x.AilmentCode),
                        SlipDamage => CreateAilment(x.SlipDamageCode),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                })
                .ToList();

            _orderView.StartAnimation(orderViewDtoList);
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
            var characterTypeId = _characterRepository.Select(characterId).Property.CharacterTypeId;
            var enemyImagePath = _enemyViewInfoFactory.Create(characterTypeId).EnemyImagePath;
            return new OrderViewDto(ItemType.Enemy, EnemyImagePath: enemyImagePath);
        }

        private OrderViewDto CreatePlayer()
        {
            return new OrderViewDto(ItemType.Player);
        }

        private OrderViewDto CreateEnemy(CharacterTypeId characterTypeId)
        {
            var enemyImagePath = _enemyViewInfoFactory.Create(characterTypeId).EnemyImagePath;
            return new OrderViewDto(ItemType.Enemy, EnemyImagePath: enemyImagePath);
        }

        private OrderViewDto CreateAilment(AilmentCode ailmentCode)
        {
            var ailmentNumber = _toAilmentNumber.Ailment(ailmentCode);
            return new OrderViewDto(ItemType.Ailment, AilmentNumber: ailmentNumber);
        }

        private OrderViewDto CreateAilment(SlipDamageCode slipDamageCode)
        {
            var ailmentNumber = _toAilmentNumber.SlipDamage(slipDamageCode);
            return new OrderViewDto(ItemType.Ailment, AilmentNumber: ailmentNumber);
        }
    }
}