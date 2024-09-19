using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using R3;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class HitPointBarViewPresenter : IObserver<CharacterEntity>
    {
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly EnemiesView _enemiesView;

        public HitPointBarViewPresenter(
            IFactory<PropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            EnemiesView enemiesView)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _enemiesView = enemiesView;
        }

        public void Observe(CharacterEntity character)
        {
            character.ReactiveCurrentHitPoint.Subscribe(x => StartHitPointBarView(character, x));
        }

        public void StartHitPointBarView(CharacterEntity character, int currentHitPoint)
        {
            if (character.IsPlayer) StartPlayerHitPointBarView(character, currentHitPoint);
            else StartEnemyHitPointBarView(character, currentHitPoint);
        }

        private void StartPlayerHitPointBarView(CharacterEntity character, int currentHitPoint)
        {
            
        }

        private void StartEnemyHitPointBarView(CharacterEntity character, int currentHitPoint)
        {
            var enemyPosition = character.Position;
            var maxHitPoint = _characterPropertyFactory.Create(character.CharacterTypeCode).HitPoint;
            var statusBarViewDto = new StatusBarViewDto(maxHitPoint, currentHitPoint);
            var dto = new EnemyHpBarViewDto(enemyPosition, statusBarViewDto);
            _enemiesView[enemyPosition].StartHitPointBarAnimationAsync(dto);
        }
    }
}