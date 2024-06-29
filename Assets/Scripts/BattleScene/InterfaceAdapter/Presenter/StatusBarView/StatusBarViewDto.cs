using System;

namespace BattleScene.InterfaceAdapter.Presenter.StatusBarView
{
    public record PlayerHpBarViewDto(
        StatusBarViewDto StatusBarViewDto);

    [Obsolete]
    public record PlayerTpBarViewDto(
        StatusBarViewDto StatusBarViewDto);

    public record EnemyHpBarViewDto(
        int EnemyInt,
        StatusBarViewDto StatusBarViewDto);

    public record StatusBarViewDto(
        int MaxPoint,
        int CurrentPoint);

    public record TechnicalPointBarViewDto(
        int MaxPoint,
        int CurrentPoint)
        : StatusBarViewDto(
            MaxPoint,
            CurrentPoint);
}