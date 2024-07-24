using System.Collections.Generic;
using BattleScene.Domain.OldId;

namespace BattleScene.UseCases.View.EnemyView.OutputData
{
    public record EnemyOutputData(
        IList<CharacterId> EnemyCharacterIdList);
}