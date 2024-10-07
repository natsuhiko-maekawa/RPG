using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IDomainService;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class OrderService
    {
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;
        private readonly IBuffDomainService _buff;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<OrderedItemEntity, OrderId> _orderedItemRepository;
        private readonly IRepository<SlipEntity, SlipDamageCode> _slipDamageRepository;

        public OrderService(
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            IFactory<PropertyValueObject, CharacterTypeCode> characterPropertyFactory, 
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository, 
            IBuffDomainService buff, 
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<OrderedItemEntity, OrderId> orderedItemRepository, 
            IRepository<SlipEntity, SlipDamageCode> slipDamageRepository)
        {
            _battlePropertyFactory = battlePropertyFactory;
            _characterPropertyFactory = characterPropertyFactory;
            _ailmentRepository = ailmentRepository;
            _buff = buff;
            _characterRepository = characterRepository;
            _orderedItemRepository = orderedItemRepository;
            _slipDamageRepository = slipDamageRepository;
        }

        public void Update()
        {
            var characterList = _characterRepository.Select().Select(x => x.Id);
            var orderedItemList = Enumerable
                .Repeat(characterList, Constant.MaxOrderNumber)
                .Select((x, i) => x
                    .Select(y => (character: y,
                        speed: _characterRepository.Select(y).ActionTime +
                               Constant.MaxAgility / GetSpeed(y) * i)))
                .SelectMany(x => x)
                .OrderBy(x => x.speed)
                .ThenByDescending(x => _characterPropertyFactory
                    .Create(_characterRepository.Select(x.character).CharacterTypeCode).Agility)
                .ThenBy(x => _characterRepository.Select(x.character).Id)
                .Select(x => new OrderedItem(x.character))
                .ToImmutableList()
                .GetRange(0, Constant.MaxOrderNumber);

            var ailments = _ailmentRepository.Select()
                .Where(x => Equals(x.CharacterId, _characterRepository.Select()
                    .First(y => y.IsPlayer)
                    .Id))
                .ToImmutableList();
            var slipDamages = _slipDamageRepository.Select();
            orderedItemList = InsertAilmentsEnd(ailments, orderedItemList);
            orderedItemList = InsertSlipDamage(slipDamages, orderedItemList);
            
            var orderedItemEntityList = orderedItemList
                .Select((x, i) => new OrderedItemEntity(
                    orderId: new OrderId(),
                    orderNumber: i,
                    orderedItem: x))
                .ToImmutableList();
            
            _orderedItemRepository.Delete();
            _orderedItemRepository.Update(orderedItemEntityList);
        }

        private ImmutableList<OrderedItem> InsertAilmentsEnd(
            IList<AilmentEntity> ailmentEntityList,
            ImmutableList<OrderedItem> order)
        {
            var newOrder = order.ToImmutableList();

            foreach (var ailmentEntity in ailmentEntityList.Where(x => x.IsSelfRecovery))
            {
                var index = ailmentEntity.Turn;
                var orderedAilmentEntity = new OrderedItem(ailmentEntity.AilmentCode);
                newOrder = newOrder
                    .Insert(index, orderedAilmentEntity)
                    .RemoveAt(order.Count - 1);
            }

            return newOrder;
        }

        private ImmutableList<OrderedItem> InsertSlipDamage(
            IReadOnlyList<SlipEntity> slipDamageEntityList,
            ImmutableList<OrderedItem> order)
        {
            var newOrder = order.ToImmutableList();
            var slipDefaultTurn = _battlePropertyFactory.Create().SlipDefaultTurn;

            foreach (var slipDamageEntity in slipDamageEntityList)
                for (var index = 0; index < newOrder.Count; ++index)
                {
                    var copiedIndex = index;
                    var characterTypeCount = newOrder
                            // TODO: Take()を使って書き換える
                        .Where((_, i) => i <= copiedIndex)
                        .Count(x => x.CharacterId != null) - 1;
                    if (slipDamageEntity.Turn != characterTypeCount % slipDefaultTurn) continue;
                    var orderedSlipDamageEntity
                        = new OrderedItem(slipDamageEntity.Id);
                    newOrder = newOrder.Insert(index, orderedSlipDamageEntity)
                        .RemoveAt(order.Count - 1);
                    ++index;
                }

            return newOrder;
        }
        
        // TODO: ActionTimeServiceにも同様のメソッドあり
        // TODO: 共通化する
        private int GetSpeed(CharacterId characterId)
        {
            var characterTypeCode = _characterRepository.Select(characterId).CharacterTypeCode;
            var agility = (float)_characterPropertyFactory.Create(characterTypeCode).Agility;
            var speedRate = _buff.GetRate(characterId, BuffCode.Speed);
            var speed = (int)Math.Ceiling(agility * speedRate);
            
            return speed;
        }
    }
}