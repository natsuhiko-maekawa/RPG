using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class FrameRepository : IFrameRepository
    {
        private readonly HashSet<FrameEntity> _frameSet = new();
        
        public FrameEntity Select(FrameNumber frameNumber)
        {
            return _frameSet.First(x => Equals(x.FrameNumber, frameNumber));
        }

        public void Update(FrameEntity frame)
        {
            _frameSet.Update(frame);
        }
    }
}