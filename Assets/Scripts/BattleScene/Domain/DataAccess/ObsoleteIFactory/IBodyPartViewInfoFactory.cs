using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DataAccess.ObsoleteIFactory
{
    public interface IBodyPartViewInfoFactory
    {
        public BodyPartViewInfoValueObject Create(BodyPartCode bodyPartCode);
    }
}