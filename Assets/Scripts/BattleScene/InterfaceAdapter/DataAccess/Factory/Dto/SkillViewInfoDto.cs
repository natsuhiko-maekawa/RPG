using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using UnityEngine;

namespace BattleScene.Domain.ValueObject
{
    [Serializable]
    public class SkillViewInfoDto : IUniqueItem<SkillCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string id;
        [SerializeField] private string skillName;
        [SerializeField] private string playerImageCode;
        [SerializeField] private string description;
        [SerializeField] private string messageCode;
        public SkillCode Id { get; private set; }
        public string SkillName { get; private set; }
        public PlayerImageCode PlayerImageCode { get; private set; }
        public MessageCode Description { get; private set; }
        public MessageCode MessageCode { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Id = Enum.Parse<SkillCode>(id);
            SkillName = skillName;
            PlayerImageCode = Enum.Parse<PlayerImageCode>(playerImageCode);
            Description = Enum.Parse<MessageCode>(description);
            MessageCode = Enum.Parse<MessageCode>(messageCode);
        }
    }
}