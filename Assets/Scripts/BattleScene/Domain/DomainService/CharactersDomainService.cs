﻿using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.Domain.DomainService
{
    public class CharactersDomainService
    {
        private readonly ICharacterRepository _characterRepository;

        public CharactersDomainService(
            ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public ImmutableList<CharacterId> GetIdList()
        {
            return _characterRepository.Select()
                .Select(x => x.CharacterId)
                .ToImmutableList();
        }

        public CharacterAggregate GetPlayer()
        {
            return _characterRepository.Select().First(x => x.IsPlayer());
        }

        public CharacterId GetPlayerId()
        {
            return _characterRepository.Select().First(x => x.IsPlayer()).CharacterId;
        }

        public ImmutableList<CharacterAggregate> GetEnemies()
        {
            return _characterRepository.Select()
                .Where(x => !x.IsPlayer())
                .ToImmutableList();
        }

        public ImmutableList<CharacterId> GetEnemiesId()
        {
            return _characterRepository.Select()
                .Where(x => !x.IsPlayer())
                .Select(x => x.CharacterId)
                .ToImmutableList();
        }
    }
}