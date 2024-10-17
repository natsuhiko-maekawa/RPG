using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class DamageViewPresenter
    {
        private readonly BattleLogDomainService _battleLog;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerView _playerView;

        public DamageViewPresenter(
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

        // public void StartDmgView(int slipDamage, ICharacter character)
        // {
        //     var tuple = (Character: character, DigitDto: new DigitDto(0, slipDamage, false));
        //     var tupleList = new List<(ICharacter, DigitDto)> { tuple };
        //     StartDigitView(tupleList, DigitColor.Orange);
        // }
        //
        // public void StartCureView(IList<CureDataStore> cureList)
        // {
        //     if (cureList.Count == 0) return;
        //     var tupleList = cureList
        //         .Select(x => (Character: x.Target, DigitDto: new DigitDto(0, x.DamageAmount, false)))
        //         .ToList();
        //     
        //     StartDigitView(tupleList, DigitColor.Green);
        // }
        //
        // private void StartDigitView(List<(ICharacter character, DigitDto digitDto)> tupleList, DigitColor digitColor)
        // {
        //     var playerDigitViewDtoList = tupleList
        //         .Where(x => x.character.IsPlayer())
        //         .Select(x => x.digitDto)
        //         .ToList();
        //     _playerView.StartPlayerDigitView(new PlayerDigitViewDto(playerDigitViewDtoList, digitColor));
        //
        //     foreach (var enemyDigitViewDto in tupleList
        //                  .Where(x => !x.character.IsPlayer())
        //                  .GroupBy(x => _enemyContainer.Get(x.character).Num)
        //                  .Select(x => new EnemyDigitViewDto(x.Key, x.Select(y => y.digitDto).ToList(), digitColor)))
        //     {
        //         _enemiesView.StartEnemyDigitView(enemyDigitViewDto);
        //     }
        // }

        public async Task StartAnimationAsync()
        {
            var taskList = new List<Task>();

            var attackList = _battleLog.GetLast().AttackList;
            var characterDigitList = attackList
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

        private DigitViewModel GetDigit(AttackValueObject attack)
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