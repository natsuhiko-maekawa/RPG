using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
{
    [Serializable]
    public class MessageDto : IUniqueItem<MessageCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string id;
        [SerializeField] private string message;
        public MessageCode Id { get; private set; }
        public string Message { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Id = Enum.Parse<MessageCode>(id);
            Message = message;
        }
    }
}