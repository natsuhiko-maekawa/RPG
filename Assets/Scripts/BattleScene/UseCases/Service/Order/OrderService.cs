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

namespace BattleScene.UseCases.Service.Order
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
                .ToList()
                .GetRange(0, Constant.MaxOrderNumber);

            var ailments = _ailmentRepository.Select()
                .Where(x => _characterRepository.Select(x.CharacterId).IsPlayer)
                .ToList();
            var slipDamages = _slipDamageRepository.Select();
            InsertAilmentEnd(ailments, ref orderedItemList);
            InsertSlipDamage(slipDamages, ref orderedItemList);
            
            var orderedItemEntityList = orderedItemList
                .Select((x, i) => new OrderedItemEntity(
                    orderId: new OrderId(),
                    orderNumber: i,
                    orderedItem: x))
                .ToImmutableList();
            
            _orderedItemRepository.Delete();
            _orderedItemRepository.Update(orderedItemEntityList);
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