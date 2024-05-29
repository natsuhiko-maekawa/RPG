// using System.Collections.Generic;
// using System.Linq;
// using BattleScene.Adapter.Container.Enemy;
// using BattleScene.Adapter.IView;
// using BattleScene.UseCase.Character;
// using BattleScene.UseCase.IPresenter;
//
// namespace BattleScene.Adapter.Presenter.InitializeView
// {
//     internal class InitializeViewPresenter : IInitializeViewPresenter
//     {
//         private readonly IEnemyContainer _enemyContainer;
//         private readonly IEnemiesView _enemiesView;
//
//         public InitializeViewPresenter(
//             IEnemyContainer enemyContainer,
//             IEnemiesView enemiesView)
//         {
//             _enemyContainer = enemyContainer;
//             _enemiesView = enemiesView;
//         }
//
//         public void Initialize(IList<ICharacter> enemyList)
//         {
//             var enemyDtoList = enemyList
//                 .Select((x, i) => new EnemyDto(
//                     enemy: x,
//                     name: x.Property.Key.ToString(),
//                     num: i))
//                 .ToList();
//             
//             _enemyContainer.Set(enemyDtoList);
//             _enemiesView.InitializeEnemyView(new InitializeViewDto(enemyDtoList));
//         }
//     }
// }