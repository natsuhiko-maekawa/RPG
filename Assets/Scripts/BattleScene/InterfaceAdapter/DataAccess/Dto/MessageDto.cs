using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Dto
{
    [Serializable]
    public class MessageDto : IUnique<MessageCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private string message;
        public MessageCode Key { get; private set; }
        public string Message { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<MessageCode>(key);
            Message = message;
        }
    }
}