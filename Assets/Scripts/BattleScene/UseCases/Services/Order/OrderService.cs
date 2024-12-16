using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.Services.Order
{
    public class OrderService
    {
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<OrderItemEntity, OrderItemId> _orderItemRepository;
        private readonly IRepository<SlipEntity, SlipCode> _slipRepository;
        private readonly ISpeedService _speed;

        public OrderService(
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<OrderItemEntity, OrderItemId> orderItemRepository,
            IRepository<SlipEntity, SlipCode> slipRepository,
            ISpeedService speed)
        {
            _battlePropertyFactory = battlePropertyFactory;
            _ailmentRepository = ailmentRepository;
            _characterRepository = characterRepository;
            _orderItemRepository = orderItemRepository;
            _slipRepository = slipRepository;
            _speed = speed;
        }

        public void Initialize()
        {
            for (var i = 0; i < _battlePropertyFactory.Create().MaxOrderCount; ++i)
            {
                var orderItemId = new OrderItemId();
                var orderItem = new OrderItemEntity(orderItemId, i);
                _orderItemRepository.Add(orderItem);
            }
        }

        public void Update()
        {
            var characters = _characterRepository.Get()
                .Where(x => x.IsSurvive);
            var orderItemList = Enumerable
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
                .Select(x => new ActorInTurn(x.character))
                .ToList()
                .GetRange(0, _battlePropertyFactory.Create().MaxOrderCount);

            var ailments = _ailmentRepository.Get()
                .Where(x => _characterRepository.Get(x.CharacterId).IsPlayer)
                .ToList();
            var slipDamages = _slipRepository.Get()
                .Where(x => x.Effects)
                .ToList();
            InsertAilmentEnd(ailments, ref orderItemList);
            InsertSlipDamage(slipDamages, ref orderItemList);

            foreach (var (orderItemEntity, orderItem) in _orderItemRepository.Get()
                         .OrderBy(x => x.Order)
                         .Zip(orderItemList, (orderItemEntity, orderItem) => (orderItemEntity, orderItem)))
            {
                orderItemEntity.SetOrderItem(orderItem);
            }
        }

        private void InsertAilmentEnd(
            IReadOnlyList<AilmentEntity> ailmentEntityList,
            ref List<ActorInTurn> order)
        {
            foreach (var ailmentEntity in ailmentEntityList.Where(x => x.IsSelfRecovery && x.Effects))
            {
                var index = ailmentEntity.Turn;
                var orderAilmentEntity = new ActorInTurn(ailmentEntity.AilmentCode);
                order.Insert(index, orderAilmentEntity);
                order.RemoveAt(order.Count - 1);
            }
        }

        private void InsertSlipDamage(
            IReadOnlyList<SlipEntity> slipEntityList,
            ref List<ActorInTurn> order)
        {
            var slipDefaultTurn = _battlePropertyFactory.Create().SlipDefaultTurn;

            foreach (var slip in slipEntityList)
            {
                for (var i = 0; i < order.Count; ++i)
                {
                    var characterTypeCount = order
                        .Take(i)
                        .Count(x => x.Actor is not null || x.SlipCode == slip.Id);
                    if (slip.Turn != characterTypeCount % slipDefaultTurn) continue;
                    var orderSlip
                        = new ActorInTurn(slip.Id);
                    order.Insert(i, orderSlip);
                    order.RemoveAt(order.Count - 1);
                    ++i;
                }
            }
        }
    }
}