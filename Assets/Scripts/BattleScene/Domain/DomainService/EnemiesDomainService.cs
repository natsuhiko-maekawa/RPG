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
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;

        public EnemiesDomainService(ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _characterCollection = characterCollection;
        }

        public ImmutableList<CharacterEntity> Get()
        {
            return _characterCollection.Get()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .ToImmutableList();
        }

        public ImmutableList<CharacterEntity> GetSurvive()
        {
            var surviveEnemyList = _characterCollection.Get()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .Where(x => x.IsSurvive)
                .ToImmutableList();
            return surviveEnemyList;
        }

        public ImmutableList<CharacterId> GetIdSurvive()
        {
            return _characterCollection.Get()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .Where(x => x.IsSurvive)
                .Select(x => x.Id)
                .ToImmutableList();
        }

        public CharacterId GetIdByPosition(int position)
        {
            return _characterCollection.Get()
                .Where(x => x.CharacterTypeCode != CharacterTypeCode.Player)
                .First(x => x.Position == position)
                .Id;
        }
    }
}