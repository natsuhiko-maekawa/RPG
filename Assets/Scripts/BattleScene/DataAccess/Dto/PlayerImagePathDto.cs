using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class PlayerImagePathDto : IUnique<PlayerImageCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string playerImageCode;
        public PlayerImageCode Key { get; private set; }
        public string PlayerImagePath { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<PlayerImageCode>(playerImageCode);
            PlayerImagePath = $"{playerImageCode}[{playerImageCode}]";
        }
    }
}