using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Aggregate
{
    public class CharacterAggregate : BaseEntity<CharacterAggregate, CharacterId>
    {
        public CharacterAggregate(
            CharacterId id,
            PropertyValueObject property)
        {
            Id = id;
            Property = property;
        }

        public override CharacterId Id { get; }
        public PropertyValueObject Property { get; }

        public bool IsPlayer()
        {
            return Property.CharacterTypeId == CharacterTypeId.Player;
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
            return Id == characterAggregate.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}