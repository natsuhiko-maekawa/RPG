using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class RestoreViewPresenter
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerView _playerView;

        public RestoreViewPresenter(
            BattleLogDomainService battleLog,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            EnemiesView enemiesView,
            PlayerView playerView)
        {
            _battleLog = battleLog;
            _characterRepository = characterRepository;
            _enemiesView = enemiesView;
            _playerView = playerView;
        }
        
        public async Task StartAnimationAsync()
        {
            var technicalPoint = _battleLog.GetLast().TechnicalPoint;
            var digit = new DigitValueObject(
                Index: 0,
                Digit: technicalPoint,
                IsAvoid: false,
                DigitColor: DigitColor.Blue);
            var digitList = new List<DigitValueObject> { digit };
            var digitViewModel = new DigitViewModel(digitList);
            await _playerView.StartPlayerDigitView(digitViewModel);
        }
    }
}