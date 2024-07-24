﻿using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using Utility.Interface;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.DomainService
{
    public class TargetDomainService
    {
        private readonly IRepository<CharacterAggregate,CharacterId> _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        private readonly IRandomEx _randomEx;

        public TargetDomainService(
            IRepository<CharacterAggregate,CharacterId> characterRepository,
            CharactersDomainService characters,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository,
            IRandomEx randomEx)
        {
            _characterRepository = characterRepository;
            _characters = characters;
            _hitPointRepository = hitPointRepository;
            _randomEx = randomEx;
        }

        public ImmutableList<CharacterId> Get(CharacterId characterId, Range range)
        {
            var targetList = range switch
            {
                Range.Random => GetRandom(characterId),
                Range.Oneself =>
                    _hitPointRepository.Select(characterId).IsSurvive()
                        ? ImmutableList<CharacterId>.Empty
                        : ImmutableList.Create(characterId),
                Range.Solo when !_characterRepository.Select(characterId).IsPlayer() =>
                    ImmutableList.Create(_characters.GetPlayer().Id),
                Range.Solo when _characterRepository.Select(characterId).IsPlayer() =>
                    throw new NotImplementedException(),
                // TODO プレイヤーが選択したターゲットを返す処理を書くこと
                _ => throw new NotImplementedException()
            };

            return targetList;
        }

        private ImmutableList<CharacterId> GetRandom(CharacterId characterId)
        {
            var targetList = _characterRepository.Select()
                .Select(x => x.Id)
                .Where(x => !Equals(x, characterId) && _hitPointRepository.Select(x).IsSurvive())
                .ToList();
            if (targetList.Count == 0) return ImmutableList<CharacterId>.Empty;
            return ImmutableList.Create(_randomEx.Choice(targetList));
        }
    }
}