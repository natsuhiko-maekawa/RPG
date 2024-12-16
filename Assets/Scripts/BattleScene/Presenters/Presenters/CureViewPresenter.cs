using System.Linq;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;

namespace BattleScene.Presenters.Presenters
{
    public class CureViewPresenter
    {
        private readonly EnemyGroupView _enemyGroupView;
        private readonly PlayerView _playerView;

        public CureViewPresenter(
            EnemyGroupView enemyGroupView,
            PlayerView playerView)
        {
            _enemyGroupView = enemyGroupView;
            _playerView = playerView;
        }

        public void StartAnimation(BattleEventEntity cureEvent)
        {
            var curingList = cureEvent.CuringList;
            var characterDigitArray = curingList
                .GroupBy(x => x.Target)
                .Select(x => (character: x.Key, model: x.Select(y => GetDigit(y))))
                .ToArray();

            var playerDigitArray = characterDigitArray
                .Where(x => x.character.IsPlayer)
                .SelectMany(x => x.model)
                .ToArray();
            var playerModel = new DigitListViewModel(playerDigitArray);
            _playerView.StartDigitAnimation(playerModel);

            var enemyDigitDict = characterDigitArray
                .Where(x => !x.character.IsPlayer)
                .ToDictionary(k => k.character.Position, v => v.model.ToArray());
            foreach (var (position, digitList) in enemyDigitDict)
            {
                var enemyModel = new DigitListViewModel(digitList);
                _enemyGroupView[position].StartDigitAnimation(enemyModel);
            }
        }

        private static DigitViewModel GetDigit(CuringValueObject curing)
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