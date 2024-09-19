using R3;

// ReSharper disable once CheckNamespace
namespace BattleScene.Domain.Entity
{
    public partial class CharacterEntity
    {
        private ReactiveProperty<int> _reactiveCurrentHitPoint;
        public Observable<int> ReactiveCurrentHitPoint => _reactiveCurrentHitPoint;
        partial void CurrentHitPointOnChange(int value) => _reactiveCurrentHitPoint = new ReactiveProperty<int>(value);
    }
}