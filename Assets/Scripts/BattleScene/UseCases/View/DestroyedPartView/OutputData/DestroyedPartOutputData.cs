using System.Collections.Generic;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.View.DestroyedPartView.OutputData
{
    public record DestroyedPartOutputData(
        CharacterOutputData Character,
        IList<int> DestroyedPartNumberList);
}