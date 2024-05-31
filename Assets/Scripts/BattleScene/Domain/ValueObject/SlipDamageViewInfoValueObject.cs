using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class SlipDamageViewInfoValueObject
    {
        public SlipDamageViewInfoValueObject(
            SlipDamageCode slipDamageCode,
            string ailmentName,
            MessageCode messageCode,
            PlayerImageCode playerImageCode)
        {
            SlipDamageCode = slipDamageCode;
            AilmentName = ailmentName;
            MessageCode = messageCode;
            PlayerImageCode = playerImageCode;
        }

        public SlipDamageCode SlipDamageCode { get; }
        public string AilmentName { get; }
        public MessageCode MessageCode { get; }
        public PlayerImageCode PlayerImageCode { get; }
    }
}