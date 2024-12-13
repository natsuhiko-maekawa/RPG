using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.Dto
{
    [Serializable]
    public class CharacterPropertyDto : IUnique<CharacterTypeCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private int hitPoint;
        [SerializeField] private int strength;
        [SerializeField] private int vitality;
        [SerializeField] private int intelligence;
        [SerializeField] private int wisdom;
        [SerializeField] private int agility;
        [SerializeField] private int luck;
        [SerializeField] private string[] skillCodeArray;
        [SerializeField] private MatAttrCode weakPointCode;
        public CharacterTypeCode Key { get; private set; }
        public int HitPoint { get; private set; }
        public int Strength { get; private set; }
        public int Vitality { get; private set; }
        public int Intelligence { get; private set; }
        public int Wisdom { get; private set; }
        public int Agility { get; private set; }
        public int Luck { get; private set; }
        public List<SkillCode> SkillCodeList { get; private set; }
        public MatAttrCode WeakPointCode { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<CharacterTypeCode>(key);
            HitPoint = hitPoint;
            Strength = strength;
            Vitality = vitality;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Agility = agility;
            Luck = luck;
            SkillCodeList = skillCodeArray.Select(Enum.Parse<SkillCode>).ToList();
            WeakPointCode = weakPointCode;
        }
    }
}