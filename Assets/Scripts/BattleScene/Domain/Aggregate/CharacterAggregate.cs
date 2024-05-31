using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Aggregate
{
    public class CharacterAggregate
    {
        public CharacterAggregate(
            CharacterId characterId,
            PropertyValueObject property)
        {
            CharacterId = characterId;
            Property = property;
        }

        public CharacterId CharacterId { get; }
        public PropertyValueObject Property { get; }

        public bool IsPlayer()
        {
            throw new NotImplementedException();
        }

        public ImmutableList<MatAttrCode> GetWeakPoints()
        {
            return Property.WeakPoints
                .ToImmutableList();
        }

        public ImmutableList<SkillCode> GetSkills()
        {
            return Property.Skills
                .ToImmutableList();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var characterAggregate = (CharacterAggregate)obj;
            return CharacterId == characterAggregate.CharacterId;
        }

        public override int GetHashCode()
        {
            return CharacterId.GetHashCode();
        }
    }
}