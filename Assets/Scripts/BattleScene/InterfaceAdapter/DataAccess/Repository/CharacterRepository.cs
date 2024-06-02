using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly HashSet<CharacterAggregate> _characterSet = new();

        public CharacterAggregate Select(CharacterId characterId)
        {
            return _characterSet.First(x => Equals(x.CharacterId, characterId));
        }

        public ImmutableList<CharacterAggregate> Select()
        {
            return _characterSet.ToImmutableList();
        }

        public void Update(CharacterAggregate character)
        {
            _characterSet.Update(character);
        }

        public void Update(IList<CharacterAggregate> characterList)
        {
            foreach (var character in characterList)
                Update(character);
        }

        public void Delete(CharacterId characterId)
        {
            _characterSet.RemoveWhere(x => Equals(x.CharacterId, characterId));
        }
    }
}