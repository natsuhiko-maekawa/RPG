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
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerView _playerView;

        public DamageViewPresenter(
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

        // public void StartDmgView(IList<DamageDataStore> dmgList)
        // {
        //     if (dmgList.Count == 0) return;
        //     var tupleList = dmgList
        //         .Select(x => (Character: x.Target, DigitDto: new DigitDto(x.AttackNum, x.DamageAmount, !x.IsHit)))
        //         .ToList();
        //     
        //     StartDigitView(tupleList, DigitColor.Orange);
        // }
        //
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
        // public void StartRestoreTpView(int tp, ICharacter character)
        // {
        //     var tuple = (Character: character, DigitDto: new DigitDto(0, tp, false));
        //     var tupleList = new List<(ICharacter, DigitDto)> { tuple };
        //     StartDigitView(tupleList, DigitColor.Blue);
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
                .Select(x => new { CharacterId = x.Key, Model = x.Select(ToModel) })
                .ToList();

            var playerDigitList = characterDigitList
                .Where(x => IsPlayer(x.CharacterId))
                .SelectMany(x => x.Model)
                .ToList();
            var playerModel = new DigitViewModel(playerDigitList);
            var startPlayerView = _playerView.StartPlayerDigitView(playerModel);
            taskList.Add(startPlayerView);

            var enemyDigitDict = characterDigitList
                .Where(x => !IsPlayer(x.CharacterId))
                .ToDictionary(k => _characterRepository.Select(k.CharacterId).Position, v => v.Model.ToList());
            foreach (var (position, digitList) in enemyDigitDict)
            {
                var enemyModel = new DigitViewModel(digitList);
                var startEnemyView = _enemiesView[position].StartDigitAnimationAsync(enemyModel);
                taskList.Add(startEnemyView);
            }

            await Task.WhenAll(taskList);
        }

        private bool IsPlayer(CharacterId characterId) => _characterRepository.Select(characterId).IsPlayer;

        private DigitValueObject ToModel(AttackValueObject attack)
        {
            var dto = new DigitValueObject(
                Index: attack.Number,
                Digit: attack.Amount,
                IsAvoid: !attack.IsHit,
                DigitColor: DigitColor.Orange);
            return dto;
        }

        // private KeyValuePair<int, DigitValueObject> ConvertPositionModelPair(AttackValueObject attack)
        // {
        //     var model = ToModel(attack);
        //     var position = _characterRepository.Select(attack.TargetId).Position;
        //     var positionModelPair = new KeyValuePair<int, DigitValueObject>(position, model);
        //     return positionModelPair;
        // }
        //
        // private DigitValueObject ConvertDto(DigitOutputData digitOutputData)
        // {
        //     return new DigitValueObject(
        //         digitOutputData.Index,
        //         digitOutputData.Digit,
        //         digitOutputData.IsAvoid,
        //         digitOutputData.DigitType switch
        //         {
        //             DigitType.DamageHp => DigitColor.Orange,
        //             DigitType.RestoreHp => DigitColor.Green,
        //             DigitType.RestoreTp => DigitColor.Blue,
        //             _ => throw new ArgumentOutOfRangeException()
        //         });
        // }
    }
}