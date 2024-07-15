using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    [Obsolete]
    public interface ICharacterRepository
    {
        public CharacterAggregate Select(CharacterId characterId);
        public ImmutableList<CharacterAggregate> Select();
        public void Update(CharacterAggregate character);
        public void Update(IList<CharacterAggregate> characterList);
        public void Delete(CharacterId characterId);
    }
}