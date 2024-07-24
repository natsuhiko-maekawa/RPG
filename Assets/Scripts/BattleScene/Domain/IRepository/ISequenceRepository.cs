using BattleScene.Domain.Entity;

namespace BattleScene.Domain.IRepository
{
    public interface ISequenceRepository
    {
        public OldSequenceEntity Select();
    }
}