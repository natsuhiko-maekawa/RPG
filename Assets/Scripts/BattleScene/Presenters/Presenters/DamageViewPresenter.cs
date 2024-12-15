using System.Linq;
using BattleScene.Domain.DomainServices;
using BattleScene.Domain.ValueObjects;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;

namespace BattleScene.Presenters.Presenters
{
    public class DamageViewPresenter
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly EnemyGroupView _enemyGroupView;
        private readonly PlayerView _playerView;

        public DamageViewPresenter(
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
            var attackList = _battleLog.GetLast().AttackList;
            var characterDigitArray = attackList
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

        private static DigitViewModel GetDigit(AttackValueObject attack)
        {
            var digit = new DigitViewModel(
                DigitType: DigitType.Damage,
                Digit: attack.Amount,
                IsAvoid: !attack.IsHit,
                Index: attack.Index);
            return digit;
        }
    }
}