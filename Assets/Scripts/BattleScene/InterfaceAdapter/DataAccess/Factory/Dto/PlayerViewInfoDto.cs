using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
{

    [Serializable]
    public class PlayerViewInfoDto : IUniqueItem<CharacterTypeId>, ISerializationCallbackReceiver
    {
        [SerializeField] private string characterTypeCode;
        [SerializeField] private string playerName;
        public CharacterTypeId Id { get; private set; }
        public string PlayerName { get; private set; }
    
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Id = Enum.Parse<CharacterTypeId>(characterTypeCode);
            PlayerName = playerName;
        }
    }
}