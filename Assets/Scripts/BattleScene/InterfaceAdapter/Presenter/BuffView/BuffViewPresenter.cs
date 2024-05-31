using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase.View.BuffView.OutputBoundary;
using BattleScene.UseCase.View.BuffView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.BuffView
{
    public class BuffViewPresenter : IBuffViewPresenter
    {
        private readonly IPlayerStatusView _playerStatusView;

        public BuffViewPresenter(
            IPlayerStatusView playerStatusView)
        {
            _playerStatusView = playerStatusView;
        }

        public void Start(BuffOutputData outputData)
        {
            if (outputData.Character.IsPlayer)
            {
                var dtoList = outputData.BuffStatusList
                    .Select(x => new BuffViewDto(x))
                    .ToImmutableList();
                _playerStatusView.StartPlayerBuffView(dtoList);
            }
        }

        public void Start(IList<BuffOutputData> outputDataList)
        {
            throw new NotImplementedException();
        }
    }
}