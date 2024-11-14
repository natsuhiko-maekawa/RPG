using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class EnhanceViewDto : IUnique<EnhanceCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private string enhanceName;
        [SerializeField] private string messageCode;
        public EnhanceCode Key { get; private set; }
        public string EnhanceName { get; private set; }
        public MessageCode MessageCode { get; private set; }
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<EnhanceCode>(key);
            EnhanceName = enhanceName;
            MessageCode = Enum.Parse<MessageCode>(messageCode);
        }
    }
}