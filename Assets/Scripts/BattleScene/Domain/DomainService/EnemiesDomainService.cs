using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.DomainService
{
    public class EnemiesDomainService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public EnemiesDomainService(IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public ImmutableList<CharacterEntity> Get()
        {
            return _characterRepository.Select()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .ToImmutableList();
        }

        public ImmutableList<CharacterEntity> GetSurvive()
        {
            var surviveEnemyList = _characterRepository.Select()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .Where(x => x.IsSurvive)
                .ToImmutableList();
            return surviveEnemyList;
        }
        
        public ImmutableList<CharacterId> GetIdSurvive()
        {
            return _characterRepository.Select()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .Where(x => x.IsSurvive)
                .Select(x => x.Id)
                .ToImmutableList();
        }

        public CharacterId GetIdByPosition(int position)
        {
            return _characterRepository.Select()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .First(x => x.Position == position)
                .Id;
        }
    }
}