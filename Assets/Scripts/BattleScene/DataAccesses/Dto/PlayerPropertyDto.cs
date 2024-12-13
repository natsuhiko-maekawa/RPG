using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.Dto
{
    [Serializable]
    public class PlayerPropertyDto : IUnique<CharacterTypeCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private CharacterTypeCode key;
        [SerializeField] private int technicalPoint;
        [SerializeField] private string[] fatalitySkillArray;
        public CharacterTypeCode Key { get; private set; }
        public int TechnicalPoint { get; private set; }
        public List<SkillCode> FatalitySkillList { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = key;
            TechnicalPoint = technicalPoint;
            FatalitySkillList = fatalitySkillArray.Select(Enum.Parse<SkillCode>).ToList();
        }
    }
}