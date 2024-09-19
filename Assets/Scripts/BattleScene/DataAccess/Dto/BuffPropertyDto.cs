using BattleScene.Domain.Code;

namespace BattleScene.DataAccess.Dto
{
    public class BuffPropertyDto
    {
        public BuffPropertyDto(
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