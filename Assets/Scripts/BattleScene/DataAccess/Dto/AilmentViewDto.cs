using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class AilmentViewDto : IUnique<(AilmentCode, SlipCode)>, ISerializationCallbackReceiver
    {
        [SerializeField] private string code;
        [SerializeField] private string name;
        [SerializeField] private string messageCode;
        [SerializeField] private string playerImageCode;
        public (AilmentCode, SlipCode) Key { get; private set; }
        public AilmentCode AilmentCode { get; private set; }
        public SlipCode SlipCode { get; private set; }
        public string Name { get; set; }
        public MessageCode MessageCode { get; private set; }
        public PlayerImageCode PlayerImageCode { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            SetCode();
            Key = (AilmentCode, SlipCode);
            Name = name;
            MessageCode = Enum.Parse<MessageCode>(messageCode);
            PlayerImageCode = Enum.Parse<PlayerImageCode>(playerImageCode);
        }

        private void SetCode()
        {
            if (Enum.TryParse<AilmentCode>(code, out var ailmentCode))
            {
                AilmentCode = ailmentCode;
                SlipCode = SlipCode.NoSlip;
                return;
            }

            if (Enum.TryParse<SlipCode>(code, out var slipDamageCode))
            {
                AilmentCode = AilmentCode.NoAilment;
                SlipCode = slipDamageCode;
                return;
            }

            throw new ArgumentException();
        }
    }
}