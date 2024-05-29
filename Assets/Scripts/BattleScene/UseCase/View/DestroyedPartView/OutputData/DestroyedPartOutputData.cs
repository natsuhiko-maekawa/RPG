using System.Collections.Generic;
using BattleScene.UseCase.Service;

namespace BattleScene.UseCase.View.DestroyedPartView.OutputData
{
    public record DestroyedPartOutputData(
        CharacterOutputData Character,
        IList<int> DestroyedPartNumberList);
}