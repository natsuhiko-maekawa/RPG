using System.Collections.Generic;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
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
            List<MatAttrCode> weakPointCodeList,
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
            WeakPointsCodeList = weakPointCodeList;
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
        public IReadOnlyList<MatAttrCode> WeakPointsCodeList { get; }
        public IReadOnlyList<SkillCode> SkillCodeList { get; }

        public int SumParameter => (int)(HitPoint / 10.0f * (Strength + Vitality + Intelligence + Agility + Luck)) / 10;
    }
}