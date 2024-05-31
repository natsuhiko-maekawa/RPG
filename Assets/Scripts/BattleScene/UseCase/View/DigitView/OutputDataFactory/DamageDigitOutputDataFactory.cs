﻿using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.View.DigitView.OutputData;

namespace BattleScene.UseCase.View.DigitView.OutputDataFactory
{
    public class DamageDigitOutputDataFactory
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemyRepository _enemyRepository;
        private readonly ResultDomainService _result;

        public ImmutableList<DigitOutputData> Create()
        {
            return _result.LastDamage().DamageList
                .Select(x => new DigitOutputData(
                    x.Number,
                    x.Amount,
                    !x.IsHit,
                    DigitType.DamageHp,
                    _characterRepository.Select(x.TargetId).IsPlayer(),
                    _characterRepository.Select(x.TargetId).IsPlayer()
                        ? default
                        : _enemyRepository.Select(x.TargetId).EnemyNumber))
                .ToImmutableList();
        }
    }
}