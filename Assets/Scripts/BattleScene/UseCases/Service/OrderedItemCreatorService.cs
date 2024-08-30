using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.UseCases.Service
{
    public class OrderedItemCreatorService
    {
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly IRepository<AilmentEntity, AilmentId> _ailmentRepository;
        private readonly IRepository<BuffEntity, BuffId> _buffRepository;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRepository<OrderedItemEntity, OrderNumber> _orderedItemRepository;
        private readonly IRepository<SlipDamageEntity, SlipDamageId> _slipDamageRepository;

        public OrderedItemCreatorService(
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            IRepository<AilmentEntity, AilmentId> ailmentRepository, 
            IRepository<BuffEntity, BuffId> buffRepository, 
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRepository<OrderedItemEntity, OrderNumber> orderedItemRepository, 
            IRepository<SlipDamageEntity, SlipDamageId> slipDamageRepository)
        {
            _actionTimeRepository = actionTimeRepository;
            _ailmentRepository = ailmentRepository;
            _buffRepository = buffRepository;
            _characterRepository = characterRepository;
            _orderedItemRepository = orderedItemRepository;
            _slipDamageRepository = slipDamageRepository;
        }

        public void Create(IList<CharacterId> characterList)
        {
            var orderedItemList = Enumerable
                .Repeat(characterList, Domain.Constant.MaxOrderNumber)
                .Select((x, i) => x
                    .Select(y => (character: y,
                        speed: _actionTimeRepository.Select(y).ActionTime +
                               Constant.MaxAgility / GetSpeed(y) * i)))
                .SelectMany(x => x)
                .OrderBy(x => x.speed)
                .ThenByDescending(x => _characterRepository.Select(x.character).Property.Agility)
                .ThenBy(x => _characterRepository.Select(x.character).Id)
                .Select(x => new OrderedItem(x.character))
                .ToImmutableList()
                .GetRange(0, Domain.Constant.MaxOrderNumber);

            var ailments = _ailmentRepository.Select()
                .Where(x => Equals(x.CharacterId, _characterRepository.Select()
                    .First(y => y.IsPlayer())
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

            foreach (var ailmentEntity in ailmentEntityList.Where(x => x.GetTurn() != null))
            {
                var index = ailmentEntity.GetTurn().GetValueOrDefault();
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
        
        private int GetSpeed(CharacterId characterId)
        {
            var speed = (float)_characterRepository.Select(characterId).Property.Agility;
            if (_buffRepository.Select()
                    .Count(x => Equals(x.CharacterId, characterId)) != 0)
            {
                var buffId = new BuffId(characterId, BuffCode.Speed);
                speed *= _buffRepository.Select(buffId)
                    .Rate;
            }
            
            return (int)Math.Ceiling(speed);
        }
    }
}