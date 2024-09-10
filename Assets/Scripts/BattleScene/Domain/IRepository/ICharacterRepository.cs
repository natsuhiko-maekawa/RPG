using System;
using System.Collections.Immutable;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    [Obsolete]
    public interface ICharacterRepository
    {
        public CharacterEntity Select(CharacterId characterId);
        public ImmutableList<CharacterEntity> Select();
    }
}