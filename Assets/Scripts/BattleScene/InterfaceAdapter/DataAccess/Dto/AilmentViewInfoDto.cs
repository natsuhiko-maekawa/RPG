using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Dto
{
    [Serializable]
    public class AilmentViewInfoDto : IUnique<(AilmentCode, SlipDamageCode)>, ISerializationCallbackReceiver
    {
        [SerializeField] private string code;
        [SerializeField] private string name;
        [SerializeField] private string messageCode;
        [SerializeField] private string playerImageCode;
        public (AilmentCode, SlipDamageCode) Key { get; private set; }
        public AilmentCode AilmentCode { get; private set; }
        public SlipDamageCode SlipDamageCode { get; private set; }
        public string Name { get; set; }
        public MessageCode MessageCode { get; private set; }
        public PlayerImageCode PlayerImageCode { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            SetCode();
            Key = (AilmentCode, SlipDamageCode);
            Name = name;
            MessageCode = Enum.Parse<MessageCode>(messageCode);
            PlayerImageCode = Enum.Parse<PlayerImageCode>(playerImageCode);
        }

        private void SetCode()
        {
            if (Enum.TryParse<AilmentCode>(code, out var ailmentCode))
            {
                AilmentCode = ailmentCode;
                SlipDamageCode = SlipDamageCode.NoSlipDamage;
                return;
            }

            if (Enum.TryParse<SlipDamageCode>(code, out var slipDamageCode))
            {
                AilmentCode = AilmentCode.NoAilment;
                SlipDamageCode = slipDamageCode;
                return;
            }

            throw new ArgumentException();
        }
    }
}