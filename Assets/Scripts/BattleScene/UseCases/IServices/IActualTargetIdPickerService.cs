using System.Collections.Generic;
using BattleScene.Domain.Entities;

namespace BattleScene.UseCases.IServices
{
    public interface IActualTargetIdPickerService
    {
        public IReadOnlyList<CharacterEntity> Pick(
            CharacterEntity actor,
            IReadOnlyList<CharacterEntity> targetList,
            float luckRate = 1.0f);
    }
}