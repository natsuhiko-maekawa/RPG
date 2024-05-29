using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCase.Service
{
    public class OrderedItemCreatorService
    {
        private readonly IActionTimeRepository _actionTimeRepository;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly ISlipDamageRepository _slipDamageRepository;
        private readonly CharactersDomainService _characters;
        private readonly AgilityToSpeedService _agilityToSpeed;

        public OrderAggregate Create(IList<CharacterId> characterList)
        {
            var iOrderedItemList = Enumerable
                .Repeat(characterList, Domain.Constant.MaxOrderNumber)
                .Select((x, i) => x
                    .Select(y => (character: y, speed: _actionTimeRepository.Select(y).ActionTime + Constant.MaxAgility / _agilityToSpeed.Convert(y) * i)))
                .SelectMany(x => x)
                .OrderBy(x => x.speed)
                .ThenBy(x => _characterRepository.Select(x.character).Property.Agility)
                .ThenBy(x => _characterRepository.Select(x.character).CharacterId)
                .Select(x => new OrderedCharacterValueObject(x.character))
                .Cast<IOrderedItem>()
                .ToImmutableList()
                .GetRange(0, Domain.Constant.MaxOrderNumber);

            var ailments = _ailmentRepository.Select(_characters.GetPlayerId());
            var slipDamages = _slipDamageRepository.Select();
            iOrderedItemList = InsertAilmentsEnd(ailments, iOrderedItemList);
            iOrderedItemList = InsertSlipDamage(slipDamages, iOrderedItemList);

            var orderedItemList = iOrderedItemList
                .Select((x, i) => new OrderedItemEntity(new OrderNumber(i), x))
                .ToImmutableList();

            return new OrderAggregate(orderedItemList);
        }

        private ImmutableList<IOrderedItem> InsertAilmentsEnd(IList<AilmentEntity> ailmentEntityList, ImmutableList<IOrderedItem> order)
        {
            var newOrder = order.ToImmutableList();

            foreach (var ailmentEntity in ailmentEntityList.Where(x => x.GetTurn() != null))
            {
                var index = ailmentEntity.GetTurn().GetValueOrDefault();
                var orderedAilmentEntity = new OrderedAilmentValueObject(ailmentEntity.AilmentCode);
                newOrder = newOrder
                    .Insert(index, orderedAilmentEntity)
                    .RemoveAt(order.Count - 1);
            }
            
            return newOrder;
        }
        
        private ImmutableList<IOrderedItem> InsertSlipDamage(IList<SlipDamageEntity> slipDamageEntityList, ImmutableList<IOrderedItem> order)
        {
            var newOrder = order.ToImmutableList();

            foreach (var slipDamageEntity in slipDamageEntityList)
            {
                for (var index = 0; index < newOrder.Count; ++index)
                {
                    var copiedIndex = index;
                    var characterTypeCount = newOrder
                        .Where((_, i) => i <= copiedIndex)
                        .Count(x => x is OrderedCharacterValueObject);
                    if (slipDamageEntity.GetTurn() != characterTypeCount % slipDamageEntity.GetTurn()) continue;
                    var orderedSlipDamageEntity 
                        = new OrderedSlipDamageValueObject(slipDamageEntity.SlipDamageCode);
                    newOrder = newOrder.Insert(index, orderedSlipDamageEntity)
                        .RemoveAt(order.Count - 1);
                    ++index;
                }
            }
            
            return newOrder;
        }
    }
}