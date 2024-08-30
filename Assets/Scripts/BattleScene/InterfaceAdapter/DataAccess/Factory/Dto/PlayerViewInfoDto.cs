using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using UnityEngine;

namespace BattleScene.Domain.ValueObject
{

    [Serializable]
    public class PlayerViewInfoDto : IUniqueItem<CharacterTypeCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string characterTypeCode;
        [SerializeField] private string playerName;
        public CharacterTypeCode Id { get; private set; }
        public string PlayerName { get; private set; }
    
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Id = Enum.Parse<CharacterTypeCode>(characterTypeCode);
            PlayerName = playerName;
        }
    }
}