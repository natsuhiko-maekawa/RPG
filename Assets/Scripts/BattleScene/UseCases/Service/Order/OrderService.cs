using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service.Order
{
    public class OrderService
    {
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly ICollection<AilmentEntity, (CharacterId, AilmentCode)> _ailmentCollection;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly ICollection<OrderedItemEntity, OrderId> _orderedItemCollection;
        private readonly ICollection<SlipEntity, SlipCode> _slipDamageCollection;
        private readonly ISpeedService _speed;

        public OrderService(
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            CharacterPropertyFactoryService characterPropertyFactory,
            ICollection<AilmentEntity, (CharacterId, AilmentCode)> ailmentCollection,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            ICollection<OrderedItemEntity, OrderId> orderedItemCollection,
            ICollection<SlipEntity, SlipCode> slipDamageCollection,
            ISpeedService speed)
        {
            _battlePropertyFactory = battlePropertyFactory;
            _characterPropertyFactory = characterPropertyFactory;
            _ailmentCollection = ailmentCollection;
            _characterCollection = characterCollection;
            _orderedItemCollection = orderedItemCollection;
            _slipDamageCollection = slipDamageCollection;
            _speed = speed;
        }

        public void Update()
        {
            var characterList = _characterCollection.Get().Select(x => x.Id);
            var orderedItemList = Enumerable
                .Repeat(characterList, _battlePropertyFactory.Create().MaxOrderCount)
                .Select((x, i) => x
                    .Select(y => (characterId: y,
                        speed: _characterCollection.Get(y).ActionTime +
                               _battlePropertyFactory.Create().MaxAgility / _speed.Get(y) * i)))
                .SelectMany(x => x)
                .OrderBy(x => x.speed)
                .ThenByDescending(x => _characterPropertyFactory
                    .Create(x.characterId).Agility)
                .ThenBy(x => _characterCollection.Get(x.characterId).Id)
                .Select(x => new OrderedItem(x.characterId))
                .ToList()
                .GetRange(0, _battlePropertyFactory.Create().MaxOrderCount);

            var ailments = _ailmentCollection.Get()
                .Where(x => _characterCollection.Get(x.CharacterId).IsPlayer)
                .ToList();
            var slipDamages = _slipDamageCollection.Get();
            InsertAilmentEnd(ailments, ref orderedItemList);
            InsertSlipDamage(slipDamages, ref orderedItemList);

            var orderedItemEntityList = orderedItemList
                .Select((x, i) => new OrderedItemEntity(
                    orderId: new OrderId(),
                    orderNumber: i,
                    orderedItem: x))
                .ToList();

            _orderedItemCollection.Remove();
            _orderedItemCollection.Add(orderedItemEntityList);
        }

        private void InsertAilmentEnd(
            IReadOnlyList<AilmentEntity> ailmentEntityList,
            ref List<OrderedItem> order)
        {
            foreach (var ailmentEntity in ailmentEntityList.Where(x => x.IsSelfRecovery && x.Effects))
            {
                var index = ailmentEntity.Turn;
                var orderedAilmentEntity = new OrderedItem(ailmentEntity.AilmentCode);
                order.Insert(index, orderedAilmentEntity);
                order.RemoveAt(order.Count - 1);
            }
        }

        private void InsertSlipDamage(
            IReadOnlyList<SlipEntity> slipEntityList,
            ref List<OrderedItem> order)
        {
            var slipDefaultTurn = _battlePropertyFactory.Create().SlipDefaultTurn;

            foreach (var slip in slipEntityList)
            {
                for (var i = 0; i < order.Count; ++i)
                {
                    var characterTypeCount = order
                        .Take(i)
                        .Count(x => x.CharacterId != null || x.SlipCode == slip.Id);
                    if (slip.Turn != characterTypeCount % slipDefaultTurn) continue;
                    var orderedSlip
                        = new OrderedItem(slip.Id);
                    order.Insert(i, orderedSlip);
                    order.RemoveAt(order.Count - 1);
                    ++i;
                }
            }
        }
    }
}