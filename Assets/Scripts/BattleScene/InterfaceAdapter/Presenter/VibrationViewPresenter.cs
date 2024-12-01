using System.Linq;
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

        public void StartAnimation()
        {
            var characterIdArray = _battleLog.GetLast().AttackList
                .Where(x => x.IsHit)
                .Select(x => x.TargetId)
                .Distinct()
                .ToArray();
            var characterList = _characterCollection.Get(characterIdArray);
            foreach (var character in characterList)
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