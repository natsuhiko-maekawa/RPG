using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.IFactory
{
    public interface IBuffViewInfoFactory
    {
        public BuffViewInfoValueObject Create(BuffCode buffCode);
    }
}