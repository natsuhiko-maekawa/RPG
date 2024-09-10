using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using UnityEngine;

namespace BattleScene.Domain.ValueObject
{
    [Serializable]
    public class SkillPropertyDto : IUnique<SkillCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private string skillName;
        [SerializeField] private string playerImageCode;
        [SerializeField] private string description;
        public SkillCode Key { get; private set; }
        public string SkillName { get; private set; }
        public PlayerImageCode PlayerImageCode { get; private set; }
        public MessageCode Description { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<SkillCode>(key);
            SkillName = skillName;
            PlayerImageCode = playerImageCode.Length == 0
                ? PlayerImageCode.NoImage
                : Enum.Parse<PlayerImageCode>(playerImageCode);
            Description = description.Length == 0
                ? MessageCode.NoMessage 
                : Enum.Parse<MessageCode>(description);
        }
    }
}