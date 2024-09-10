using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class CharacterEntity : BaseEntity<CharacterEntity, CharacterId>
    {
        public CharacterEntity(
            CharacterId id,
            PropertyValueObject property)
        {
            Id = id;
            Property = property;
            CharacterTypeCode = property.CharacterTypeCode;
        }

        public CharacterEntity(
            CharacterId id,
            CharacterTypeCode characterTypeCode,
            int currentHitPoint,
            int currentTechnicalPoint,
            int actionTime)
        {
            Id = id;
            CharacterTypeCode = characterTypeCode;
            CurrentHitPoint = currentHitPoint;
            CurrentTechnicalPoint = currentTechnicalPoint;
            ActionTime = actionTime;
        }
        
        public override CharacterId Id { get; }
        public CharacterTypeCode CharacterTypeCode { get; }
        // TODO: 仮の値を設定
        public int CurrentHitPoint { get; } = 1;
        public int CurrentTechnicalPoint { get; }
        public int ActionTime { get; }
        public bool IsSurvive => 0 < CurrentHitPoint;

        [Obsolete]
        public PropertyValueObject Property { get; }

        [Obsolete]
        public bool IsPlayer()
        {
            return Property.CharacterTypeCode == CharacterTypeCode.Player;
        }

        [Obsolete]
        public ImmutableList<MatAttrCode> GetWeakPoints()
        {
            return Property.WeakPoints
                .ToImmutableList();
        }
    }
}