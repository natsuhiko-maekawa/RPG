using System;
using BattleScene.InterfaceAdapter.Presenter.Dto.Interface;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.Presenter.FrameView
{
    public record FrameViewDto(
        Color Color) : IDto;
    
    [Obsolete]
    public record PlayerFrameViewDto(
        Color Color);

    [Obsolete]
    public record EnemyFrameViewDto(
        int EnemyInt,
        Color Color);
}