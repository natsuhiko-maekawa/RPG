using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Dto
{
    [Serializable]
    public class AilmentViewInfoDto : IUnique<AilmentCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string ailmentCode;
        [SerializeField] private string ailmentName;
        [SerializeField] private string messageCode;
        [SerializeField] private string playerImageCode;
        public AilmentCode Key { get; private set; }
        public string AilmentName { get; set; }
        public MessageCode MessageCode { get; private set; }
        public PlayerImageCode PlayerImageCode { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<AilmentCode>(ailmentCode);
            AilmentName = ailmentName;
            MessageCode = Enum.Parse<MessageCode>(messageCode);
            PlayerImageCode = Enum.Parse<PlayerImageCode>(playerImageCode);
        }
    }
}