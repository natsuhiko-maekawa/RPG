using System;
using System.Linq;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.Infrastructure.Factory.Dto
{
    [Serializable]
    public class PropertyDataStore : ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        public string name;
        public int hp;
        public int strength;
        public int vitality;
        public int intelligence;
        public int wisdom;
        public int agility;
        public int luck;
        [SerializeField] private string[] skills;
        [SerializeField] private string[] weakPoints;
        public CharacterTypeId Key { get; private set; }
        public SkillCode[] Skills { get; private set; }
        public MatAttrCode[] WeakPoints { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<CharacterTypeId>(key);
            Skills = skills.Select(Enum.Parse<SkillCode>).ToArray();
            WeakPoints = weakPoints.Select(Enum.Parse<MatAttrCode>).ToArray();
        }
    }
}