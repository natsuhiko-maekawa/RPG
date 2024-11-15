using System.Collections.Generic;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.IService
{
    public interface IActualTargetIdPickerService
    {
        public IReadOnlyList<CharacterId> Pick(
            IReadOnlyList<CharacterId> targetIdList,
            float luckRate = 1.0f);
    }
}