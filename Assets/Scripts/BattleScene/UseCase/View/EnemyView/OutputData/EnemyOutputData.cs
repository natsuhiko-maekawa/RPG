using System.Collections.Generic;
using BattleScene.Domain.Id;

namespace BattleScene.UseCase.View.EnemyView.OutputData
{
    public record EnemyOutputData(
        IList<CharacterId> EnemyCharacterIdList);
}