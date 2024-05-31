using System;
using System.Linq;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.Infrastructure.Factory.Dto
{
    [Serializable]
    public class PlayerPropertyDto : ISerializationCallbackReceiver
    {
        public int tp;
        [SerializeField] private string[] fatalitySkills;
        public SkillCode[] FatalitySkills { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            FatalitySkills = fatalitySkills.Select(Enum.Parse<SkillCode>).ToArray();
        }
    }
}