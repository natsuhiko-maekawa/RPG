﻿using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.DomainServices;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;

namespace BattleScene.UseCases.Services
{
    public class BuffTurnService
    {
        private readonly OrderItemsDomainService _orderItems;
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;
        private readonly IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> _enhanceRepository;

        public BuffTurnService(
            OrderItemsDomainService orderItems,
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository,
            IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> enhanceRepository)
        {
            _orderItems = orderItems;
            _buffRepository = buffRepository;
            _enhanceRepository = enhanceRepository;
        }

        public void Advance()
        {
            foreach (var buff in _buffRepository.Get()
                         .Where(x => x.LifetimeCode == LifetimeCode.ToEndTurn || IsNextAction(x.LifetimeCode)))
            {
                buff.AdvanceTurn();
            }

            foreach (var enhance in _enhanceRepository.Get()
                         .Where(x => x.LifetimeCode == LifetimeCode.ToEndTurn || IsNextAction(x.LifetimeCode)))
            {
                enhance.AdvanceTurn();
            }
        }

        private bool IsNextAction(LifetimeCode lifetimeCode)
        {
            if (lifetimeCode != LifetimeCode.ToNextAction) return false;
            if (!_orderItems.First().TryGetActor(out var character)) return false;
            if (!character.IsPlayer) return false;
            return true;
        }
    }
}