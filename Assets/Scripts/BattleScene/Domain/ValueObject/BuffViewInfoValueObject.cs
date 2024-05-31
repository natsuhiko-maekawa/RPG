using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class BuffViewInfoValueObject
    {
        public BuffViewInfoValueObject(
            BuffCode buffCode,
            MessageCode messageCode)
        {
            BuffCode = buffCode;
            MessageCode = messageCode;
        }

        public BuffCode BuffCode { get; }
        public MessageCode MessageCode { get; }
    }
}