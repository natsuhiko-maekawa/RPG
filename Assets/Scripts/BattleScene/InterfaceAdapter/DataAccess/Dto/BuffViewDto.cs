using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Dto
{
    [Serializable]
    public class BuffViewDto: IUnique<BuffCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private string buffName;
        [SerializeField] private string messageCode;
        public BuffCode Key { get; private set; }
        public string BuffName { get; private set; }
        public MessageCode MessageCode { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<BuffCode>(key);
            BuffName = buffName;
            MessageCode = Enum.Parse<MessageCode>(messageCode);
        }
    }
}