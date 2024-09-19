using R3;

// ReSharper disable once CheckNamespace
namespace BattleScene.Domain.Entity
{
    public partial class AilmentEntity
    {
        private ReactiveProperty<bool> _reactiveEffects;
        public Observable<bool> ReactiveEffects => _reactiveEffects;
        partial void EffectsOnChange(bool value) => _reactiveEffects = new ReactiveProperty<bool>(value);
    }
}