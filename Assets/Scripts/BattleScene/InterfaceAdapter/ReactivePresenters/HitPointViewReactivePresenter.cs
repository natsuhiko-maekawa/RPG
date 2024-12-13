using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.ViewModels;
using BattleScene.Framework.Views;
using R3;

namespace BattleScene.InterfaceAdapter.ReactivePresenters
{
    public class HitPointViewReactivePresenter : IReactive<CharacterEntity>
    {
        private readonly IFactory<CharacterPropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly EnemyGroupView _enemyGroupView;
        private readonly PlayerView _playerView;

        public HitPointViewReactivePresenter(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            EnemyGroupView enemyGroupView,
            PlayerView playerView)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _enemyGroupView = enemyGroupView;
            _playerView = playerView;
        }

        public void Observe(CharacterEntity character)
        {
            character.ReactiveCurrentHitPoint.Subscribe(x => StartHitPointBarView(character, x));
        }

        private void StartHitPointBarView(CharacterEntity character, int currentHitPoint)
        {
            if (character.IsPlayer) StartPlayerHitPointBarView(currentHitPoint);
            else StartEnemyHitPointBarView(character, currentHitPoint);
        }

        private void StartPlayerHitPointBarView(int currentHitPoint)
        {
            var maxHitPoint = _characterPropertyFactory.Create(CharacterTypeCode.Player).HitPoint;
            var model = new StatusBarViewModel(maxHitPoint, currentHitPoint);
            _playerView.StartHitPointBarAnimation(model);
        }

        private void StartEnemyHitPointBarView(CharacterEntity character, int currentHitPoint)
        {
            var enemyPosition = character.Position;
            var maxHitPoint = _characterPropertyFactory.Create(character.CharacterTypeCode).HitPoint;
            var model = new StatusBarViewModel(maxHitPoint, currentHitPoint);
            _enemyGroupView[enemyPosition].StartHitPointBarAnimation(model);
        }
    }
}