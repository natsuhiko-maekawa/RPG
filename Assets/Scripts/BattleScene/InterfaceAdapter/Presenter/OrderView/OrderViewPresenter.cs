using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.OrderView.OutputBoundary;
using BattleScene.UseCase.View.OrderView.OutputData;
using static BattleScene.UseCase.View.OrderView.OutputData.OrderOutputDataType;

namespace BattleScene.InterfaceAdapter.Presenter.OrderView
{
    internal class OrderViewPresenter : IOrderViewPresenter
    {
        private readonly IEnemyViewInfoFactory _enemyViewInfoFactory;
        private readonly IOrderView _orderView;
        private readonly ToAilmentNumberService _toAilmentNumber;

        public OrderViewPresenter(
            IEnemyViewInfoFactory enemyViewInfoFactory,
            IOrderView orderView,
            ToAilmentNumberService toAilmentNumber)
        {
            _enemyViewInfoFactory = enemyViewInfoFactory;
            _orderView = orderView;
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