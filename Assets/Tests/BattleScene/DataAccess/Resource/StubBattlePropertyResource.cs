using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;

namespace Tests.BattleScene.DataAccess.Resource
{
    public class StubBattlePropertyResource : IResource<BattlePropertyDto>
    {
        private readonly BattlePropertyDto _dto = new();

        public BattlePropertyDto Get()
        {
            return _dto;
        }
    }
}