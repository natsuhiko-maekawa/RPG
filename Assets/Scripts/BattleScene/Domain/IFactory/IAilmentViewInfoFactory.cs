using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.IFactory
{
    public interface IAilmentViewInfoFactory
    {
        public AilmentViewInfoValueObject Create(AilmentCode ailmentCode);
    }
}