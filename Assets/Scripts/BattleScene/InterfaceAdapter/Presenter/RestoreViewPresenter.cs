using System.Collections.Generic;
using BattleScene.Domain.DomainService;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class RestoreViewPresenter
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly PlayerView _playerView;

        public RestoreViewPresenter(
            BattleLogDomainService battleLog,
            PlayerView playerView)
        {
            _battleLog = battleLog;
            _playerView = playerView;
        }

        public void StartAnimation()
        {
            var technicalPoint = _battleLog.GetLast().TechnicalPoint;
            var digit = new DigitViewModel(
                DigitType: DigitType.Restore,
                Digit: technicalPoint);
            var digitList = new List<DigitViewModel> { digit };
            var digitViewModel = new DigitListViewModel(digitList);
            _playerView.StartPlayerDigitView(digitViewModel);
        }
    }
}