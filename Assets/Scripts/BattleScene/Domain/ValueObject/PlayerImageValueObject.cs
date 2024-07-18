using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
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