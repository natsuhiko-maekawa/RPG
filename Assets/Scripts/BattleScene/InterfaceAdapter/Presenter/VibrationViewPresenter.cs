using System.Linq;
using System.Threading.Tasks;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Framework.View;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class VibrationViewPresenter
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerView _playerView;

        public VibrationViewPresenter(
            BattleLogDomainService battleLog,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            EnemiesView enemiesView,
            PlayerView playerView)
        {
            _battleLog = battleLog;
            _characterCollection = characterCollection;
            _enemiesView = enemiesView;
            _playerView = playerView;
        }

        public async Task StartAnimationAsync()
        {
            var characterIdList = _battleLog.GetLast().ActualTargetIdList;
            var characterList = characterIdList
                .Select(_characterCollection.Get)
                .ToList();
            var taskList = characterList
                .Select(x => x!.IsPlayer
                    ? StartPlayerAnimation()
                    : StartEnemyAnimationAsync(x.Position))
                .ToList();
            await Task.WhenAll(taskList);
        }

        private async Task StartPlayerAnimation()
        {
            _playerView.StartPlayerVibeView();
        }

        private async Task StartEnemyAnimationAsync(int position)
        {
            await _enemiesView[position].StartVibesAnimationAsync();
        }
    }
}