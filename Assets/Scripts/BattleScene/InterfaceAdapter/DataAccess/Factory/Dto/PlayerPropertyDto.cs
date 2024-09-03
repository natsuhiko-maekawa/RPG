using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
{
    [Serializable]
    public class PlayerPropertyDto : IUniqueItem<CharacterTypeCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private CharacterTypeCode key;
        [SerializeField] private int technicalPoint;
        [SerializeField] private string[] fatalitySkills;
        public CharacterTypeCode Id { get; private set; }
        public int TechnicalPoint { get; private set; }
        public SkillCode[] FatalitySkills { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Id = key;
            TechnicalPoint = technicalPoint;
            FatalitySkills = fatalitySkills.Select(Enum.Parse<SkillCode>).ToArray();
        }
    }
}