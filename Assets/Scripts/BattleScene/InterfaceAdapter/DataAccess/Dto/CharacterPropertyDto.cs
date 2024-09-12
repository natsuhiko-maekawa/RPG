using System;
using System.Linq;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Dto
{
    [Serializable]
    public class CharacterPropertyDto : IUnique<CharacterTypeCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string characterTypeCode;
        [SerializeField] private int hitPoint;
        [SerializeField] private int strength;
        [SerializeField] private int vitality;
        [SerializeField] private int intelligence;
        [SerializeField] private int wisdom;
        [SerializeField] private int agility;
        [SerializeField] private int luck;
        [SerializeField] private string[] skills;
        [SerializeField] private string[] weakPoints;
        public CharacterTypeCode Key { get; private set; }
        public int HitPoint { get; private set; }
        public int Strength { get; private set; }
        public int Vitality { get; private set; }
        public int Intelligence { get; private set; }
        public int Wisdom { get; private set; }
        public int Agility { get; private set; }
        public int Luck { get; private set; }
        public SkillCode[] Skills { get; private set; }
        public MatAttrCode[] WeakPoints { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<CharacterTypeCode>(characterTypeCode);
            HitPoint = hitPoint;
            Strength = strength;
            Vitality = vitality;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Agility = agility;
            Luck = luck;
            Skills = skills.Select(Enum.Parse<SkillCode>).ToArray();
            WeakPoints = weakPoints.Select(Enum.Parse<MatAttrCode>).ToArray();
        }
    }
}