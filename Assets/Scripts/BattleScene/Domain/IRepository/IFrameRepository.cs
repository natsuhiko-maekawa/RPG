using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    public interface IFrameRepository
    {
        public FrameEntity Select(FrameNumber frameNumber);
        public void Update(FrameEntity frame);
    }
}