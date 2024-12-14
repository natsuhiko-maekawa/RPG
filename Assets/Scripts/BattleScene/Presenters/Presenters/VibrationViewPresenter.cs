using System.Linq;
using BattleScene.Domain.DomainServices;
using BattleScene.Views.Views;

namespace BattleScene.Presenters.Presenters
{
    public class VibrationViewPresenter
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly EnemyGroupView _enemyGroupView;
        private readonly PlayerView _playerView;

        public VibrationViewPresenter(
            BattleLogDomainService battleLog,
            EnemyGroupView enemyGroupView,
            PlayerView playerView)
        {
            _battleLog = battleLog;
            _enemyGroupView = enemyGroupView;
            _playerView = playerView;
        }

        public void StartAnimation()
        {
            var characterArray = _battleLog.GetLast().AttackList
                .Where(x => x.IsHit)
                .Select(x => x.Target)
                .Where(x => !x.IsPlayer)
                .Distinct()
                .ToArray();
            foreach (var character in characterArray)
            {
                StartEnemyAnimation(character.Position);
            }
        }

        private void StartEnemyAnimation(int position)
        {
            _enemyGroupView[position].StartVibeAnimation();
        }
    }
}