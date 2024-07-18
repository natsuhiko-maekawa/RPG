using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using UnityEngine;

namespace BattleScene.Domain.ValueObject
{
    [Serializable]
    public class PlayerImageValueObject : IUniqueItem<PlayerImageCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string playerImageCode;
        public PlayerImageCode Id { get; private set; }
        public string PlayerImagePath { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Id = Enum.Parse<PlayerImageCode>(playerImageCode);
            PlayerImagePath = $"{playerImageCode}[{playerImageCode}]";
        }
    }
}