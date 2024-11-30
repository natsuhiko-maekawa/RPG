using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;

namespace Tests.BattleScene.UseCases.Service
{
    public class MockActualTargetIdPickerService : IActualTargetIdPickerService
    {
        public IReadOnlyList<CharacterId> Pick(
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            float luckRate = 1)
        {
            return targetIdList;
        }
    }
}