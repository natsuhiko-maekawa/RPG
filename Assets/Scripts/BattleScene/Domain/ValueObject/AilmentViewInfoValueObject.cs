using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class AilmentViewInfoValueObject
    {
        public AilmentViewInfoValueObject(
            AilmentCode ailmentCode,
            string ailmentName,
            MessageCode messageCode,
            PlayerImageCode playerImageCode)
        {
            AilmentCode = ailmentCode;
            AilmentName = ailmentName;
            MessageCode = messageCode;
            PlayerImageCode = playerImageCode;
        }

        public AilmentCode AilmentCode { get; }
        public string AilmentName { get; }
        public MessageCode MessageCode { get; }
        public PlayerImageCode PlayerImageCode { get; }
    }
}