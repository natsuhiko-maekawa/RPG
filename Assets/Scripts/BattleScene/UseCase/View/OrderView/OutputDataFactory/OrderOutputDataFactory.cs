using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.OrderView.OutputData;

namespace BattleScene.UseCase.View.OrderView.OutputDataFactory
{
    internal class OrderOutputDataFactory
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemyViewInfoFactory _enemyViewInfoFactory;
        private readonly IOrderRepository _orderRepository;
        private readonly ToAilmentNumberService _toAilmentNumber;

        public ImmutableList<OrderOutputData> Create()
        {
            return _orderRepository.Select().OrderedItemList
                .Select(x =>
                {
                    return x.OrderedItem switch
                    {
                        OrderedCharacterValueObject orderedCharacter
                            when _characterRepository.Select(orderedCharacter.CharacterId).IsPlayer()
                            => new OrderOutputData(
                                OrderOutputDataType.Player,
                                default,
                                default),
                        OrderedCharacterValueObject orderedCharacter
                            when !_characterRepository.Select(orderedCharacter.CharacterId).IsPlayer()
                            => new OrderOutputData(
                                OrderOutputDataType.Enemy,
                                _enemyViewInfoFactory
                                    .Create(_characterRepository
                                        .Select(orderedCharacter.CharacterId)
                                        .Property.CharacterTypeId).EnemyImagePath,
                                default),
                        OrderedAilmentValueObject orderedAilment => new OrderOutputData(
                            OrderOutputDataType.Ailment,
                            default,
                            _toAilmentNumber
                                .Ailment(orderedAilment.AilmentCode)),
                        OrderedSlipDamageValueObject orderedSlipDamage => new OrderOutputData(
                            OrderOutputDataType.Ailment,
                            default,
                            _toAilmentNumber
                                .SlipDamage(orderedSlipDamage.SlipDamageCode)),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                })
                .ToImmutableList();
        }
    }
}