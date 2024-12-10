using System.Collections.Generic;
using BattleScene.Domain.Entity;

namespace BattleScene.UseCases.IService
{
    public interface IActualTargetIdPickerService
    {
        public IReadOnlyList<CharacterEntity> Pick(
            CharacterEntity actor,
            IReadOnlyList<CharacterEntity> targetList,
            float luckRate = 1.0f);
    }
}