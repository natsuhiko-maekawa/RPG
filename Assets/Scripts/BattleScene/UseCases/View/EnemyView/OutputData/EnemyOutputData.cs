using System.Collections.Generic;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.View.EnemyView.OutputData
{
    public record EnemyOutputData(
        IList<CharacterId> EnemyCharacterIdList);
}