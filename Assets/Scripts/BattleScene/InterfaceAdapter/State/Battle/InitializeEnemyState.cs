using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.UseCases.IService;
using static BattleScene.Domain.Code.CharacterTypeCode;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    internal class InitializeEnemyState : AbstractState
    {
        private readonly IEnemiesRegistererService _enemiesRegisterer;
        private readonly EnemyImagePresenter _enemyImage;
        private readonly OrderState _orderState;

        public InitializeEnemyState(
            IEnemiesRegistererService enemiesRegisterer,
            EnemyImagePresenter enemyImage,
            OrderState orderState)
        {
            _enemiesRegisterer = enemiesRegisterer;
            _enemyImage = enemyImage;
            _orderState = orderState;
        }

        public override void Start()
        {
            SetEnemies();
            _enemyImage.Show();
            Context.TransitionTo(_orderState);
        }

        private void SetEnemies()
        {
            var enemyTypeIdList = new List<CharacterTypeCode> { Bee, Dragon, Mantis, Shuten, Slime };
            _enemiesRegisterer.Register(enemyTypeIdList);
        }
    }
}