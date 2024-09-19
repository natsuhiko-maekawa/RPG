using System;
using System.Linq;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class PlayerPropertyDto : IUnique<CharacterTypeCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private CharacterTypeCode key;
        [SerializeField] private int technicalPoint;
        [SerializeField] private string[] fatalitySkills;
        public CharacterTypeCode Key { get; private set; }
        public int TechnicalPoint { get; private set; }
        public SkillCode[] FatalitySkills { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = key;
            TechnicalPoint = technicalPoint;
            FatalitySkills = fatalitySkills.Select(Enum.Parse<SkillCode>).ToArray();
        }
    }
}