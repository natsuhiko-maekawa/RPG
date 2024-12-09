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
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<OrderedItemEntity, OrderedItemId> _orderedItemRepository;
        private readonly IRepository<SlipEntity, SlipCode> _slipRepository;
        private readonly ISpeedService _speed;

        public OrderService(
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<OrderedItemEntity, OrderedItemId> orderedItemRepository,
            IRepository<SlipEntity, SlipCode> slipRepository,
            ISpeedService speed)
        {
            _battlePropertyFactory = battlePropertyFactory;
            _ailmentRepository = ailmentRepository;
            _characterRepository = characterRepository;
            _orderedItemRepository = orderedItemRepository;
            _slipRepository = slipRepository;
            _speed = speed;
        }

        public void Initialize()
        {
            for (var i = 0; i < _battlePropertyFactory.Create().MaxOrderCount; ++i)
            {
                var orderedItemId = new OrderedItemId();
                var orderedItem = new OrderedItemEntity(orderedItemId, i);
                _orderedItemRepository.Add(orderedItem);
            }
        }

        public void Update()
        {
            var characters = _characterRepository.Get();
            var orderedItemList = Enumerable
                .Repeat(characters, _battlePropertyFactory.Create().MaxOrderCount)
                .Select((charactersRepeat, i) => charactersRepeat
                    .Select(character => (character,
                        speed: character.ActionTime +
                               _battlePropertyFactory.Create().MaxAgility / _speed.GetSpeed(character.Id) * i)))
                .SelectMany(x => x)
                .OrderBy(x => x.speed)
                .ThenByDescending(x => _speed.GetAgility(x.character.Id))
                .ThenBy(x => x.character.CharacterTypeCode)
                .ThenBy(x => x.character.Id)
                .Select(x => new OrderedItem(x.character.Id))
                .ToList()
                .GetRange(0, _battlePropertyFactory.Create().MaxOrderCount);

            var ailments = _ailmentRepository.Get()
                .Where(x => _characterRepository.Get(x.CharacterId).IsPlayer)
                .ToList();
            var slipDamages = _slipRepository.Get()
                .Where(x => x.Effects)
                .ToList();
            InsertAilmentEnd(ailments, ref orderedItemList);
            InsertSlipDamage(slipDamages, ref orderedItemList);

            // QUESTION: IEnumeratorを渡した場合とToArrayして配列を渡した場合、アロケーションが少ないのはどちらか。
            foreach (var (orderedItemEntity, orderedItem) in _orderedItemRepository.Get()
                         .OrderBy(x => x.Order)
                         .Zip(orderedItemList, (orderedItemEntity, orderedItem) => (orderedItemEntity, orderedItem))
                         .ToArray())
            {
                orderedItemEntity.SetOrderedItem(orderedItem);
            }
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