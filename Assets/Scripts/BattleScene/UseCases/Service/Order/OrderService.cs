using System.Collections.Generic;
using System.Collections.Immutable;
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
        private readonly ICollection<SlipEntity, SlipDamageCode> _slipDamageCollection;
        private readonly ISpeedService _speed;

        public OrderService(
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            CharacterPropertyFactoryService characterPropertyFactory, 
            ICollection<AilmentEntity, (CharacterId, AilmentCode)> ailmentCollection, 
            ICollection<CharacterEntity, CharacterId> characterCollection,
            ICollection<OrderedItemEntity, OrderId> orderedItemCollection, 
            ICollection<SlipEntity, SlipDamageCode> slipDamageCollection,
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
                .Repeat(characterList, Constant.MaxOrderNumber)
                .Select((x, i) => x
                    .Select(y => (characterId: y,
                        speed: _characterCollection.Get(y).ActionTime +
                               Constant.MaxAgility / _speed.Get(y) * i)))
                .SelectMany(x => x)
                .OrderBy(x => x.speed)
                .ThenByDescending(x => _characterPropertyFactory
                    .Create(x.characterId).Agility)
                .ThenBy(x => _characterCollection.Get(x.characterId).Id)
                .Select(x => new OrderedItem(x.characterId))
                .ToList()
                .GetRange(0, Constant.MaxOrderNumber);

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
                .ToImmutableList();
            
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
            IReadOnlyList<SlipEntity> slipDamageEntityList,
            ref List<OrderedItem> order)
        {
            var slipDefaultTurn = _battlePropertyFactory.Create().SlipDefaultTurn;

            foreach (var slipDamageEntity in slipDamageEntityList)
                for (var index = 0; index < order.Count; ++index)
                {
                    var copiedIndex = index;
                    var characterTypeCount = order
                            // TODO: Take()を使って書き換える
                        .Where((_, i) => i <= copiedIndex - 1)
                        .Count(x => x.CharacterId != null);
                    if (slipDamageEntity.Turn != characterTypeCount % (slipDefaultTurn + 1)) continue;
                    var orderedSlipDamageEntity
                        = new OrderedItem(slipDamageEntity.Id);
                    order.Insert(index, orderedSlipDamageEntity); 
                    order.RemoveAt(order.Count - 1);
                    ++index;
                }
        }
    }
}