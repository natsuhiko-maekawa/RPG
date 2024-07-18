using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
{

    [Serializable]
    public class PlayerViewInfoValueObject : IUniqueItem<CharacterTypeId>, ISerializationCallbackReceiver
    {
        [SerializeField] private string playerImageCode;
        [SerializeField] private string playerName;
        public CharacterTypeId Id { get; private set; }
        public string PlayerName { get; private set; }
    
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Id = Enum.Parse<CharacterTypeId>(playerImageCode);
            PlayerName = playerName;
        }
    }
    
}