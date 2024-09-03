using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DataAccess.ObsoleteIFactory
{
    public interface IBuffViewInfoFactory
    {
        public BuffViewInfoValueObject Create(BuffCode buffCode);
    }
}