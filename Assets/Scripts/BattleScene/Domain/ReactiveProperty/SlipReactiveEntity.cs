using R3;

// ReSharper disable once CheckNamespace
namespace BattleScene.Domain.Entity
{
    public partial class SlipEntity
    {
        private readonly ReactiveProperty<bool> _reactiveEffects= new();
        public Observable<bool> ReactiveEffects => _reactiveEffects;
        partial void EffectsOnChange(bool value) => _reactiveEffects.Value = value ;
    }
}