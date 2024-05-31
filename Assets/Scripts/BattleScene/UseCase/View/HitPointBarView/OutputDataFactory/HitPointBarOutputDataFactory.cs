﻿using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.HitPointBarView.OutputData;

namespace BattleScene.UseCase.View.HitPointBarView.OutputDataFactory
{
    public class HitPointBarOutputDataFactory
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemyRepository _enemyRepository;
        private readonly IHitPointRepository _hitPointRepository;
        private readonly ResultDomainService _result;

        public ImmutableList<HitPointBarOutputData> Create()
        {
            return _result.LastDamage().DamageList
                .GroupBy(x => x.TargetId)
                .Where(x => x.Any(y => y.IsHit))
                .Select(x => x
                    .Select(y => (targetId: y.TargetId, digit: y.Amount))
                    .Aggregate((y, z) => (y.targetId, y.digit + z.digit)))
                .Select(x => new HitPointBarOutputData(
                    _characterRepository.Select(x.targetId).IsPlayer()
                        ? CharacterOutputData.SetPlayer()
                        : CharacterOutputData.SetEnemy(_enemyRepository.Select(x.targetId).EnemyNumber),
                    _hitPointRepository.Select(x.targetId).GetMax(),
                    _hitPointRepository.Select(x.targetId).GetCurrent()))
                .ToImmutableList();
        }
    }
}