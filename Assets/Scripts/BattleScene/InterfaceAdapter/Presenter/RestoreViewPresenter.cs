using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        public async Task StartAnimationAsync()
        {
            var technicalPoint = _battleLog.GetLast().TechnicalPoint;
            var digit = new Digit(
                DigitType: DigitType.Restore,
                Number: technicalPoint);
            var digitList = new List<Digit> { digit };
            var digitViewModel = new DigitViewModel(digitList);
            await _playerView.StartPlayerDigitView(digitViewModel);
        }
    }
}