using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.Domain.DomainService
{
    public class CharactersDomainService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public CharactersDomainService(
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public ImmutableList<CharacterId> GetIdList()
        {
            return _characterRepository.Select()
                .Select(x => x.Id)
                .ToImmutableList();
        }

        [Obsolete]
        public CharacterEntity GetPlayer()
        {
            return _characterRepository.Select().First(x => x.IsPlayer());
        }

        [Obsolete]
        public CharacterId GetPlayerId()
        {
            return _characterRepository.Select().First(x => x.IsPlayer()).Id;
        }

        public ImmutableList<CharacterEntity> GetEnemies()
        {
            return _characterRepository.Select()
                .Where(x => !x.IsPlayer())
                .ToImmutableList();
        }

        public ImmutableList<CharacterId> GetEnemiesId()
        {
            return _characterRepository.Select()
                .Where(x => !x.IsPlayer())
                .Select(x => x.Id)
                .ToImmutableList();
        }
    }
}