using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.State.Turn;
using BattleScene.UseCases.IService;
using static BattleScene.Domain.Code.CharacterTypeCode;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class InitializeEnemyState : BaseState
    {
        private readonly IEnemiesRegistererService _enemiesRegisterer;
        private readonly EnemyImagePresenter _enemyImage;
        private readonly TurnState _turnState;

        public InitializeEnemyState(
            IEnemiesRegistererService enemiesRegisterer,
            EnemyImagePresenter enemyImage,
            TurnState turnState)
        {
            _enemiesRegisterer = enemiesRegisterer;
            _enemyImage = enemyImage;
            _turnState = turnState;
        }

        public override void Start()
        {
            SetEnemies();
            _enemyImage.Show();
            Context.TransitionTo(_turnState);
        }

        private void SetEnemies()
        {
            var enemyTypeIdList = new List<CharacterTypeCode> { Bee, Dragon, Mantis, Shuten, Slime };
            _enemiesRegisterer.Register(enemyTypeIdList);
        }
    }
}