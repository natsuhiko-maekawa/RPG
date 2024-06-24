using System.Collections.Generic;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.View.BuffView.OutputData
{
    public record BuffOutputData(
        CharacterOutputData Character,
        IList<int> BuffStatusList);
}