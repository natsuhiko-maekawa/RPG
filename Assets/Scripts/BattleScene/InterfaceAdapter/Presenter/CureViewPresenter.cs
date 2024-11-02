using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class CureViewPresenter
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerView _playerView;

        public CureViewPresenter(
            ICollection<CharacterEntity, CharacterId> characterCollection,
            EnemiesView enemiesView,
            PlayerView playerView)
        {
            _characterCollection = characterCollection;
            _enemiesView = enemiesView;
            _playerView = playerView;
        }

        public async Task StartAnimationAsync(BattleEventValueObject cure)
        {
            var taskList = new List<Task>();

            var curingList = cure.CuringList;
            var characterDigitList = curingList
                .GroupBy(x => x.TargetId)
                .Select(x => new { CharacterId = x.Key, Model = x.Select(GetDigit) })
                .ToList();

            var playerDigitList = characterDigitList
                .Where(x => IsPlayer(x.CharacterId))
                .SelectMany(x => x.Model)
                .ToList();
            var playerModel = new DigitListViewModel(playerDigitList);
            var startPlayerView = _playerView.StartPlayerDigitView(playerModel);
            taskList.Add(startPlayerView);

            var enemyDigitDict = characterDigitList
                .Where(x => !IsPlayer(x.CharacterId))
                .ToDictionary(k => _characterCollection.Get(k.CharacterId).Position, v => v.Model.ToList());
            foreach (var (position, digitList) in enemyDigitDict)
            {
                var enemyModel = new DigitListViewModel(digitList);
                var startEnemyView = _enemiesView[position].StartDigitAnimationAsync(enemyModel);
                taskList.Add(startEnemyView);
            }

            await Task.WhenAll(taskList);
        }

        private bool IsPlayer(CharacterId characterId) => _characterCollection.Get(characterId).IsPlayer;

        private DigitViewModel GetDigit(CuringValueObject curing)
        {
            var digit = new DigitViewModel(
                DigitType: DigitType.Cure,
                Digit: curing.Amount,
                IsAvoid: false,
                Index: 0);
            return digit;
        }
    }
}