﻿using System;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.Dto
{
    [Serializable]
    public class SkillViewDto : IUnique<SkillCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private string name;
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
            SkillName = name;
            PlayerImageCode = playerImageCode.Length == 0
                ? PlayerImageCode.NoImage
                : Enum.Parse<PlayerImageCode>(playerImageCode);
            Description = description.Length == 0
                ? MessageCode.NoMessage
                : Enum.Parse<MessageCode>(description);
        }
    }
}