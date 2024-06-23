using System.Collections.Generic;

namespace BattleScene.InterfaceAdapter.Presenter.EnemyView
{
    public record EnemyViewDto(
        int EnemyCount,
        IList<EnemyDto> EnemyDtoList);

    public record EnemyDto(
        int EnemyNumber,
        string EnemyImagePath);
}