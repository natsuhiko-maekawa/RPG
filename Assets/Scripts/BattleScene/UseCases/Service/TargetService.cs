using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Service
{
    public class TargetService : ITargetService
    {
        private readonly IRepository<BattleEventEntity, BattleEventId> _battleLogRepository;
        private readonly EnemiesDomainService _enemies;
        private readonly PlayerDomainService _player;
        private readonly IMyRandomService _myRandom;

        public TargetService(
            EnemiesDomainService enemies,
            IRepository<BattleEventEntity, BattleEventId> battleLogRepository,
            PlayerDomainService player,
            IMyRandomService myRandom)
        {
            _enemies = enemies;
            _battleLogRepository = battleLogRepository;
            _player = player;
            _myRandom = myRandom;
        }

        public IReadOnlyList<CharacterEntity> Get(CharacterEntity actor, Range range, bool isAutoTarget=false)
        {
            var targetList = range switch
            {
                Range.Oneself =>
                    actor.IsSurvive
                        ? new[] { actor }
                        : Array.Empty<CharacterEntity>(),
                Range.Solo when isAutoTarget =>
                    actor.IsPlayer
                        ? new[] { GetEnemySoloRandom() }
                        : new[] { _player.Get() },
                Range.Solo when !isAutoTarget =>
                    actor.IsPlayer
                        ? new[] { GetEnemySolo() }
                        : new[] { _player.Get() },
                Range.Line or Range.Random =>
                    actor.IsPlayer
                        ? _enemies.GetSurvive()
                        : new[] { _player.Get() },
                _ => throw new NotImplementedException()
            };

            return targetList;
        }

        public void GetOption(CharacterEntity actor, Range range, List<CharacterEntity> optionTargetList)
        {
            optionTargetList.Clear();
            var player = _player.Get();
            if (range == Range.Player || !(actor.IsPlayer ^ range == Range.Oneself))
            {
                optionTargetList.Add(player);
            }
            else
            {
                var optionTargets = _enemies.GetSurvive();
                optionTargetList.AddRange(optionTargets);
            }
        }

        private CharacterEntity GetEnemySolo()
        {
            var target = _battleLogRepository.Get()
                .Where(static x => x.Actor?.IsPlayer ?? false)
                .Where(static x => x.TargetList.Count == 1)
                .Where(static x => x.TargetList
                    .Select(x => x.Id)
                    .Contains(x.Actor!.Id))
                .Max()?.TargetList
                .Single();
            target = target is not { IsSurvive: true }
                ? _enemies.Get()
                    .OrderBy(static enemy => enemy.Position)
                    .First(static enemy => enemy.IsSurvive)
                : target;
            return target;
        }

        private CharacterEntity GetEnemySoloRandom()
        {
            var enemyArray = _enemies.Get()
                .Where(static enemy => enemy.IsSurvive)
                .ToArray();
            var enemy = _myRandom.Choice(enemyArray);
            return enemy;
        }
    }
}