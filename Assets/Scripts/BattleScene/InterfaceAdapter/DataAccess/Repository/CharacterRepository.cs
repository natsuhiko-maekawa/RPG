using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly HashSet<CharacterEntity> _characterSet = new();

        public CharacterEntity Select(CharacterId characterId)
        {
            return _characterSet.First(x => Equals(x.Id, characterId));
        }

        public ImmutableList<CharacterEntity> Select()
        {
            return _characterSet.ToImmutableList();
        }

        public void Update(CharacterEntity character)
        {
            _characterSet.Update(character);
        }

        public void Update(IList<CharacterEntity> characterList)
        {
            foreach (var character in characterList)
                Update(character);
        }

        public void Delete(CharacterId characterId)
        {
            _characterSet.RemoveWhere(x => Equals(x.Id, characterId));
        }
    }
}