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

        public void StartAnimation()
        {
            var characterIdArray = _battleLog.GetLast().AttackList
                .Where(x => x.IsHit)
                .Select(x => x.TargetId)
                .Distinct()
                .ToArray();
            var characterList = _characterRepository.Get(characterIdArray);
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