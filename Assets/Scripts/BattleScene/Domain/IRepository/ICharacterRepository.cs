using System;
using System.Collections.Immutable;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.IRepository
{
    [Obsolete]
    public interface ICharacterRepository
    {
        public CharacterAggregate Select(CharacterId characterId);
        public ImmutableList<CharacterAggregate> Select();
    }
}