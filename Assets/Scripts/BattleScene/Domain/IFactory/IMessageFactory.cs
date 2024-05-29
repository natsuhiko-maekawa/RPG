using BattleScene.Domain.Code;

namespace BattleScene.Domain.IFactory
{
    public interface IMessageFactory
    {
        public string GetMessage(MessageCode messageCode);
    }
}