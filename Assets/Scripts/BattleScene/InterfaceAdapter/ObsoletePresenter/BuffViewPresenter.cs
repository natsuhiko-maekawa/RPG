using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.UseCases.View.BuffView.OutputBoundary;
using BattleScene.UseCases.View.BuffView.OutputData;

namespace BattleScene.InterfaceAdapter.ObsoletePresenter
{
    public class BuffViewPresenter : IBuffViewPresenter
    {
        private readonly PlayerStatusView _playerStatusView;

        public BuffViewPresenter(
            PlayerStatusView playerStatusView)
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