﻿using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Dto
{

    [Serializable]
    public class PlayerViewInfoDto : IUnique<CharacterTypeCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string characterTypeCode;
        [SerializeField] private string playerName;
        public CharacterTypeCode Key { get; private set; }
        public string PlayerName { get; private set; }
    
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<CharacterTypeCode>(characterTypeCode);
            PlayerName = playerName;
        }
    }
}