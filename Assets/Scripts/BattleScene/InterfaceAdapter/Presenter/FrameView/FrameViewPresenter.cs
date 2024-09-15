using System;
using System.Collections.Generic;
using BattleScene.InterfaceAdapter.Interface;
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
            {
                var color = ToColor(outputData.FrameType);
                var dto = new FrameViewDto(color);
                
                if (outputData.IsPlayer)
                {
                    _playerView.StartFrameView(dto);
                }
                else
                {
                    var enemyPosition = outputData.EnemyNumber;
                    _enemiesView[enemyPosition].StartFrameAnimationAsync(dto);
                }
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