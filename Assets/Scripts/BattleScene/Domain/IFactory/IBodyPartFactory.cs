using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IFactory
{
    public interface IBodyPartFactory
    {
        public BodyPartEntity Create(CharacterId characterId, BodyPartCode bodyPartCode);
    }
}