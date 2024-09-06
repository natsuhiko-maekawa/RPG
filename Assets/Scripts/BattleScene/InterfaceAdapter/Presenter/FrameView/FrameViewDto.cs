using System;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.Presenter.FrameView
{
    public record FrameViewDto(
        Color Color);
    
    [Obsolete]
    public record PlayerFrameViewDto(
        Color Color);

    [Obsolete]
    public record EnemyFrameViewDto(
        int EnemyInt,
        Color Color);
}