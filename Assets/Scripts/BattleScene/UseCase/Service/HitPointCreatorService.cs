using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;

namespace BattleScene.UseCase.Service
{
    public class HitPointCreatorService
    {
        public HitPointAggregate Create(CharacterAggregate character)
        {
            var characterId = character.CharacterId;
            var hitPoint = character.Property.HitPoint;
            return new HitPointAggregate(characterId, hitPoint);
        }

        public ImmutableList<HitPointAggregate> Create(IList<CharacterAggregate> characterList)
        {
            return characterList
                .Select(Create)
                .ToImmutableList();
        }
    }
}