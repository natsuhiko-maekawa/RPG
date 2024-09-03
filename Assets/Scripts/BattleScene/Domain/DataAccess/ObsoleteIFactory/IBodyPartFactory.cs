using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.DataAccess.ObsoleteIFactory
{
    public interface IBodyPartFactory
    {
        public BodyPartEntity Create(CharacterId characterId, BodyPartCode bodyPartCode);
    }
}