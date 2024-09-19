using System.Collections.Immutable;
using System.Linq;
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

        public ImmutableList<CharacterEntity> GetEnemies()
        {
            return _characterRepository.Select()
                .Where(x => !x.IsPlayer)
                .ToImmutableList();
        }
    }
}