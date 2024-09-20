﻿// using System;
// using System.Collections.Generic;
// using System.Collections.Immutable;
// using System.Linq;
// using BattleScene.Framework.View;
// using BattleScene.Framework.ViewModel;
// using BattleScene.UseCases.View.DigitView.OutputBoundary;
// using BattleScene.UseCases.View.DigitView.OutputData;
//
// namespace BattleScene.InterfaceAdapter.ObsoletePresenter
// {
//     internal class DigitViewPresenter : IDigitViewPresenter
//     {
//         private readonly EnemiesView _enemiesView;
//         private readonly PlayerView _playerView;
//
//         public DigitViewPresenter(
//             EnemiesView enemiesView,
//             PlayerView playerView)
//         {
//             _enemiesView = enemiesView;
//             _playerView = playerView;
//         }
//
//         public void StartDmgView(IList<DamageDataStore> dmgList)
//         {
//             if (dmgList.Count == 0) return;
//             var tupleList = dmgList
//                 .Select(x => (Character: x.Target, DigitDto: new DigitDto(x.AttackNum, x.DamageAmount, !x.IsHit)))
//                 .ToList();
//             
//             StartDigitView(tupleList, DigitColor.Orange);
//         }
//         
//         public void StartDmgView(int slipDamage, ICharacter character)
//         {
//             var tuple = (Character: character, DigitDto: new DigitDto(0, slipDamage, false));
//             var tupleList = new List<(ICharacter, DigitDto)> { tuple };
//             StartDigitView(tupleList, DigitColor.Orange);
//         }
//         
//         public void StartCureView(IList<CureDataStore> cureList)
//         {
//             if (cureList.Count == 0) return;
//             var tupleList = cureList
//                 .Select(x => (Character: x.Target, DigitDto: new DigitDto(0, x.DamageAmount, false)))
//                 .ToList();
//             
//             StartDigitView(tupleList, DigitColor.Green);
//         }
//         
//         public void StartRestoreTpView(int tp, ICharacter character)
//         {
//             var tuple = (Character: character, DigitDto: new DigitDto(0, tp, false));
//             var tupleList = new List<(ICharacter, DigitDto)> { tuple };
//             StartDigitView(tupleList, DigitColor.Blue);
//         }
//         
//         private void StartDigitView(List<(ICharacter character, DigitDto digitDto)> tupleList, DigitColor digitColor)
//         {
//             var playerDigitViewDtoList = tupleList
//                 .Where(x => x.character.IsPlayer())
//                 .Select(x => x.digitDto)
//                 .ToList();
//             _playerView.StartPlayerDigitView(new PlayerDigitViewDto(playerDigitViewDtoList, digitColor));
//         
//             foreach (var enemyDigitViewDto in tupleList
//                          .Where(x => !x.character.IsPlayer())
//                          .GroupBy(x => _enemyContainer.Get(x.character).Num)
//                          .Select(x => new EnemyDigitViewDto(x.Key, x.Select(y => y.digitDto).ToList(), digitColor)))
//             {
//                 _enemiesView.StartEnemyDigitView(enemyDigitViewDto);
//             }
//         }
//
//         public void Start(IList<DigitOutputData> digitOutputDataList)
//         {
//             var playerDigitViewDtoList = digitOutputDataList
//                 .Where(x => x.IsPlayer)
//                 .Select(ConvertDto)
//                 .ToList();
//             _playerView.StartPlayerDigitView(new PlayerDigitViewDto(playerDigitViewDtoList));
//
//             foreach (var enemyDigitViewDto in digitOutputDataList
//                          .Where(x => !x.IsPlayer)
//                          .GroupBy(x => x.EnemyNumber)
//                          .Select(x => new EnemyDigitViewDto(x.Key, x.Select(ConvertDto).ToImmutableList())))
//                 _enemiesView[enemyDigitViewDto.EnemyInt].StartDigitAnimationAsync(enemyDigitViewDto);
//         }
//
//         private DigitValueObject ConvertDto(DigitOutputData digitOutputData)
//         {
//             return new DigitValueObject(
//                 digitOutputData.Index,
//                 digitOutputData.Digit,
//                 digitOutputData.IsAvoid,
//                 digitOutputData.DigitType switch
//                 {
//                     DigitType.DamageHp => DigitColor.Orange,
//                     DigitType.RestoreHp => DigitColor.Green,
//                     DigitType.RestoreTp => DigitColor.Blue,
//                     _ => throw new ArgumentOutOfRangeException()
//                 });
//         }
//     }
// }