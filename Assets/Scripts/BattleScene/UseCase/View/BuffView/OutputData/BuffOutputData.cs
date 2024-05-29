using System.Collections.Generic;
using BattleScene.UseCase.Service;

namespace BattleScene.UseCase.View.BuffView.OutputData
{
    public record BuffOutputData(
        CharacterOutputData Character,
        IList<int> BuffStatusList);
}