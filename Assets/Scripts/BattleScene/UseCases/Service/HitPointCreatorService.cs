using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;

namespace BattleScene.UseCases.Service
{
    public class HitPointCreatorService
    {
        public HitPointAggregate Create(CharacterEntity character)
        {
            var characterId = character.Id;
            var hitPoint = character.Property.HitPoint;
            return new HitPointAggregate(characterId, hitPoint);
        }

        public ImmutableList<HitPointAggregate> Create(IList<CharacterEntity> characterList)
        {
            return characterList
                .Select(Create)
                .ToImmutableList();
        }
    }
}