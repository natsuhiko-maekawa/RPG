﻿using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.DomainService
{
    public class SlipDomainService
    {
        private readonly IRepository<SlipEntity, SlipDamageCode> _slipRepository;

        public SlipDomainService(
            IRepository<SlipEntity, SlipDamageCode> slipRepository)
        {
            _slipRepository = slipRepository;
        }

        public void AdvanceTurn()
        {
            var slip = _slipRepository.Select()
                .Select(x =>
                {
                    x.AdvanceTurn();
                    return x;
                })
                .ToImmutableList();
            _slipRepository.Update(slip);
        }
    }
}