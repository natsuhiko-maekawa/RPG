using System;
using System.Collections.Generic;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCases.View.FrameView.OutputBoundary;
using BattleScene.UseCases.View.FrameView.OutputData;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.Presenter.FrameView
{
    internal class FrameViewPresenter : IFrameViewPresenter
    {
        private readonly IEnemiesView _enemiesView;
        private readonly IPlayerView _playerView;

        public void Start(IList<FrameOutputData> outputDataList)
        {
            foreach (var outputData in outputDataList)
                if (outputData.IsPlayer)
                {
                    var dto = new PlayerFrameViewDto(ToColor(outputData.FrameType));
                    _playerView.StartPlayerFrameView(dto);
                }
                else
                {
                    var dto = new EnemyFrameViewDto(outputData.EnemyNumber, ToColor(outputData.FrameType));
                    _enemiesView.StartEnemyFrameView(dto);
                }
        }

        public void Stop()
        {
            _playerView.StopPlayerFrameView();
            _enemiesView.StopEnemyFrameView();
        }

        private Color ToColor(FrameType frameType)
        {
            return frameType switch
            {
                FrameType.Actor => Color.white,
                FrameType.Target => Color.red,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}