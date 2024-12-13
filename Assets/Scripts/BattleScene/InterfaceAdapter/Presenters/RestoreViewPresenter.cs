using System.Collections.Generic;
using BattleScene.Domain.DomainServices;
using BattleScene.Framework.ViewModels;
using BattleScene.Framework.Views;

namespace BattleScene.InterfaceAdapter.Presenters
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
            _playerView.StartDigitAnimation(digitViewModel);
        }
    }
}