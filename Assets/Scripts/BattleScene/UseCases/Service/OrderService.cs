using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class OrderService
    {
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;
        private readonly BuffDomainService _buff;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<OrderedItemEntity, OrderNumber> _orderedItemRepository;
        private readonly IRepository<SlipDamageEntity, SlipDamageId> _slipDamageRepository;

        public OrderService(
            IFactory<PropertyValueObject, CharacterTypeCode> characterPropertyFactory, 
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository, 
            BuffDomainService buff, 
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<OrderedItemEntity, OrderNumber> orderedItemRepository, 
            IRepository<SlipDamageEntity, SlipDamageId> slipDamageRepository)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _ailmentRepository = ailmentRepository;
            _buff = buff;
            _characterRepository = characterRepository;
            _orderedItemRepository = orderedItemRepository;
            _slipDamageRepository = slipDamageRepository;
        }

        public void Update(IList<CharacterId> characterList)
        {
            var orderedItemList = Enumerable
                .Repeat(characterList, Domain.Constant.MaxOrderNumber)
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
                .GetRange(0, Domain.Constant.MaxOrderNumber);

            var ailments = _ailmentRepository.Select()
                .Where(x => Equals(x.CharacterId, _characterRepository.Select()
                    .First(y => y.IsPlayer)
                    .Id))
                .ToImmutableList();
            var slipDamages = _slipDamageRepository.Select();
            orderedItemList = InsertAilmentsEnd(ailments, orderedItemList);
            orderedItemList = InsertSlipDamage(slipDamages, orderedItemList);
            
            var orderedItemEntityList = orderedItemList
                .Select((x, i) => new OrderedItemEntity(new OrderNumber(i), x))
                .ToImmutableList();
            
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
            IList<SlipDamageEntity> slipDamageEntityList,
            ImmutableList<OrderedItem> order)
        {
            var newOrder = order.ToImmutableList();

            foreach (var slipDamageEntity in slipDamageEntityList)
                for (var index = 0; index < newOrder.Count; ++index)
                {
                    var copiedIndex = index;
                    var characterTypeCount = newOrder
                        .Where((_, i) => i <= copiedIndex)
                        .Count(x => x.CharacterId != null);
                    if (slipDamageEntity.GetTurn() != characterTypeCount % slipDamageEntity.GetTurn()) continue;
                    var orderedSlipDamageEntity
                        = new OrderedItem(slipDamageEntity.SlipDamageCode);
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