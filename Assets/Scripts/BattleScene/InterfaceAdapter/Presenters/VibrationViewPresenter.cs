using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Framework.Views;

namespace BattleScene.InterfaceAdapter.Presenters
{
    public class VibrationViewPresenter
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerView _playerView;

        public VibrationViewPresenter(
            BattleLogDomainService battleLog,
            EnemiesView enemiesView,
            PlayerView playerView)
        {
            _battleLog = battleLog;
            _enemiesView = enemiesView;
            _playerView = playerView;
        }

        public void StartAnimation()
        {
            var characterArray = _battleLog.GetLast().AttackList
                .Where(x => x.IsHit)
                .Select(x => x.Target)
                .Distinct()
                .ToArray();
            foreach (var character in characterArray)
            {
                if (character.IsPlayer)
                {
                    // StartPlayerAnimation();
                }
                else
                {
                    StartEnemyAnimation(character.Position);
                }
            }
        }

        private void StartPlayerAnimation()
        {
            _playerView.StartPlayerVibeView();
        }

        private void StartEnemyAnimation(int position)
        {
            _enemiesView[position].StartVibeAnimation();
        }
    }
}