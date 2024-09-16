using System.Collections.Immutable;
using System.Linq;
using BattleScene.Framework.View;
using BattleScene.UseCases.View.DestroyedPartView.OutputBoundary;
using BattleScene.UseCases.View.DestroyedPartView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.DestroyedPartView
{
    public class DestroyedPartViewPresenter : IDestroyedPartViewPresenter
    {
        private readonly PlayerStatusView _playerStatusView;

        public DestroyedPartViewPresenter(
            PlayerStatusView playerStatusView)
        {
            _playerStatusView = playerStatusView;
        }

        public void Start(DestroyedPartOutputData outputData)
        {
            if (outputData.Character.IsPlayer)
            {
                var dtoList = outputData.DestroyedPartNumberList
                    .Select(x => new PlayerDestroyedPartViewDto(x))
                    .ToImmutableList();
                _playerStatusView.StartPlayerDestroyedPartView(dtoList);
            }
        }
    }
}