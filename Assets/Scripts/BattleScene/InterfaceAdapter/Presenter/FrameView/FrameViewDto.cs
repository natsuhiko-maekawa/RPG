using UnityEngine;

namespace BattleScene.InterfaceAdapter.Presenter.FrameView
{
    public record PlayerFrameViewDto(
        Color Color);

    public record EnemyFrameViewDto(
        int EnemyInt,
        Color Color);
}