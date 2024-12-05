using System.Linq;
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
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerView _playerView;

        public CureViewPresenter(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            EnemiesView enemiesView,
            PlayerView playerView)
        {
            _characterRepository = characterRepository;
            _enemiesView = enemiesView;
            _playerView = playerView;
        }

        public void StartAnimation(BattleEventValueObject cure)
        {
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
            _playerView.StartPlayerDigitView(playerModel);

            var enemyDigitDict = characterDigitList
                .Where(x => !IsPlayer(x.CharacterId))
                .ToDictionary(k => _characterRepository.Get(k.CharacterId).Position, v => v.Model.ToList());
            foreach (var (position, digitList) in enemyDigitDict)
            {
                var enemyModel = new DigitListViewModel(digitList);
                _enemiesView[position].StartDigitAnimation(enemyModel);
            }
        }

        private bool IsPlayer(CharacterId characterId) => _characterRepository.Get(characterId).IsPlayer;

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