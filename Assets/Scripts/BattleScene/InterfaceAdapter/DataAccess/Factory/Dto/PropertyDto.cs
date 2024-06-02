using System;
using System.Linq;
using BattleScene.Domain.Code;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
{
    [Serializable]
    public class PropertyDto : ISerializationCallbackReceiver
    {
        [SerializeField] private string characterTypeId;
        public int hp;
        public int strength;
        public int vitality;
        public int intelligence;
        public int wisdom;
        public int agility;
        public int luck;
        [SerializeField] private string[] skills;
        [SerializeField] private string[] weakPoints;
        public CharacterTypeId CharacterTypeId { get; private set; }
        public SkillCode[] Skills { get; private set; }
        public MatAttrCode[] WeakPoints { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            CharacterTypeId = Enum.Parse<CharacterTypeId>(characterTypeId);
            Skills = skills.Select(Enum.Parse<SkillCode>).ToArray();
            WeakPoints = weakPoints.Select(Enum.Parse<MatAttrCode>).ToArray();
        }
    }
}