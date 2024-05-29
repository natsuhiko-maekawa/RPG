using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class PropertyValueObject
    { 
        public CharacterTypeId CharacterTypeId { get; }
        public int HitPoint { get; }
        public int Strength { get; }
        public int Vitality { get; }
        public int Intelligence { get; }
        public int Wisdom { get; }
        public int Agility { get; }
        public int Luck { get; }
        public ImmutableList<MatAttrCode> WeakPoints { get; }
        public ImmutableList<SkillCode> Skills { get; }
        
        public PropertyValueObject(CharacterTypeId characterTypeId, int hitPoint, int strength, int vitality, int intelligence, 
            int wisdom, int agility, int luck, MatAttrCode[] weakPoints, SkillCode[] skills)
        {
            CharacterTypeId = characterTypeId;
            HitPoint = hitPoint;
            Strength = strength;
            Vitality = vitality;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Agility = agility;
            Luck = luck;
            WeakPoints = ImmutableList.Create(weakPoints);
            Skills = ImmutableList.Create(skills);
        }
    }
}