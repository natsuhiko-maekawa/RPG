using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.View.OrderView.OutputData;

namespace BattleScene.UseCase.View.OrderView.OutputDataFactory
{
    internal class OrderOutputDataFactory
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderOutputDataFactory(
            ICharacterRepository characterRepository,
            IOrderRepository orderRepository)
        {
            _characterRepository = characterRepository;
            _orderRepository = orderRepository;
        }

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
                                OrderOutputDataType.Player),
                        OrderedCharacterValueObject orderedCharacter
                            when !_characterRepository.Select(orderedCharacter.CharacterId).IsPlayer()
                            => new OrderOutputData(
                                OrderOutputDataType.Enemy,
                                CharacterTypeId: _characterRepository.Select(orderedCharacter.CharacterId).Property.CharacterTypeId),
                        OrderedAilmentValueObject orderedAilment => new OrderOutputData(
                            OrderOutputDataType.Ailment,
                            AilmentCode: orderedAilment.AilmentCode),
                        OrderedSlipDamageValueObject orderedSlipDamage => new OrderOutputData(
                            OrderOutputDataType.Ailment,
                            SlipDamageCode: orderedSlipDamage.SlipDamageCode),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                })
                .ToImmutableList();
        }
    }
}