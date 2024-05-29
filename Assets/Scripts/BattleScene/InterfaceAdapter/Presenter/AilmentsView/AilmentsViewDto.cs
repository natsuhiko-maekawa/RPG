using System.Collections.Generic;

namespace BattleScene.InterfaceAdapter.Presenter.AilmentsView
{
    public record PlayerAilmentsViewDto(
        int AilmentsInt);
    
    public record EnemyAilmentsViewDto(
        int EnemyInt, 
        IList<AilmentsDto> AilmentsDtoList);

    public record AilmentsDto(
        int AilmentsInt);
}