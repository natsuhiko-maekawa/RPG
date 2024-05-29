using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.IFactory
{
    public interface IBodyPartViewInfoFactory
    {
        public BodyPartViewInfoValueObject Create(BodyPartCode bodyPartCode);
    }
}