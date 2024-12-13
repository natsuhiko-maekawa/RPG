using System.Collections.Generic;
using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public class CharacterPropertyValueObject
    {
        public CharacterPropertyValueObject(
            CharacterTypeCode characterTypeCode,
            int hitPoint,
            int strength,
            int vitality,
            int intelligence,
            int wisdom,
            int agility,
            int luck,
            MatAttrCode weakPointCode,
            List<SkillCode> skillCodeList)
        {
            CharacterTypeCode = characterTypeCode;
            HitPoint = hitPoint;
            Strength = strength;
            Vitality = vitality;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Agility = agility;
            Luck = luck;
            WeakPointsCode = weakPointCode;
            SkillCodeList = skillCodeList;
        }

        public CharacterTypeCode CharacterTypeCode { get; }
        public int HitPoint { get; }
        public int Strength { get; }
        public int Vitality { get; }
        public int Intelligence { get; }
        public int Wisdom { get; }
        public int Agility { get; }
        public int Luck { get; }
        public MatAttrCode WeakPointsCode { get; }
        public IReadOnlyList<SkillCode> SkillCodeList { get; }

        public int SumParameter => (int)(HitPoint / 10.0f * (Strength + Vitality + Intelligence + Agility + Luck)) / 10;
    }
}