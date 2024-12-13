using System;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.Dto
{
    [Serializable]
    public class BuffViewDto : IUnique<BuffCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private string name;
        [SerializeField] private string messageCode;
        public BuffCode Key { get; private set; }
        public string Name { get; private set; }
        public MessageCode MessageCode { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<BuffCode>(key);
            Name = name;
            MessageCode = Enum.Parse<MessageCode>(messageCode);
        }
    }
}