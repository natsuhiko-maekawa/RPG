using System;
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

        public CharacterAggregate(
            CharacterId id,
            PointValueObject hitPoint,
            PointValueObject technicalPoint,
            int actionTime)
        {
            Id = id;
            HitPoint = hitPoint;
            TechnicalPoint = technicalPoint;
            ActionTime = actionTime;
        }
        
        public PointValueObject HitPoint { get; }
        public PointValueObject TechnicalPoint { get; }
        public int ActionTime { get; }

        public override CharacterId Id { get; }
        [Obsolete]
        public PropertyValueObject Property { get; }

        [Obsolete]
        public bool IsPlayer()
        {
            return Property.CharacterTypeId == CharacterTypeId.Player;
        }

        [Obsolete]
        public ImmutableList<MatAttrCode> GetWeakPoints()
        {
            return Property.WeakPoints
                .ToImmutableList();
        }

        [Obsolete]
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