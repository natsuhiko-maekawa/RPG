using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
{
    [Serializable]
    public class AilmentViewInfoDto : IUniqueItem<AilmentCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string ailmentCode;
        public string ailmentName;
        [SerializeField] private string messageCode;
        [SerializeField] private string playerImageCode;
        public AilmentCode Id { get; private set; }
        public MessageCode MessageCode { get; private set; }
        public PlayerImageCode PlayerImageCode { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Id = Enum.Parse<AilmentCode>(ailmentCode);
            MessageCode = Enum.Parse<MessageCode>(messageCode);
            PlayerImageCode = Enum.Parse<PlayerImageCode>(playerImageCode);
        }
    }
}