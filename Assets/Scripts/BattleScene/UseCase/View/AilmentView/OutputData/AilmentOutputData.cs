using System.Collections.Generic;

namespace BattleScene.UseCase.View.AilmentView.OutputData
{
    public record AilmentOutputData(
        bool IsPlayer,
        int EnemyNumber,
        IList<int> AilmentNumberList);
}