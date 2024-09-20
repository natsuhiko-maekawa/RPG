using System.Linq;
using System.Threading.Tasks;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Framework.View;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class VibrationViewPresenter
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerView _playerView;

        public VibrationViewPresenter(
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

        public async void StartAnimationAsync()
        {
            var characterIdList = _battleLog.GetLast().ActualTargetIdList;
            var characterList = characterIdList
                .Select(_characterRepository.Select)
                .ToList();
            var taskList = characterList
                .Select(x => x.IsPlayer 
                    ? StartPlayerAnimationAsync()
                    : StartEnemyAnimationAsync(x.Position))
                .ToList();
            await Task.WhenAll(taskList);
        }

        private async Task StartPlayerAnimationAsync()
        {
            await _playerView.StartPlayerVibesView();
        }

        private async Task StartEnemyAnimationAsync(int position)
        {
            await _enemiesView[position].StartVibesAnimationAsync();
        }
    }
}