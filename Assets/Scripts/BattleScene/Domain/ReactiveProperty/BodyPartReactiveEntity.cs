using R3;

// ReSharper disable once CheckNamespace
namespace BattleScene.Domain.Entity
{
    public partial class BodyPartEntity
    {
        private readonly ReactiveProperty<int> _reactiveDestroyedCount = new();
        public Observable<int> ReactiveDestroyedCount => _reactiveDestroyedCount;
        partial void DestroyedCountOnChange(int value) => _reactiveDestroyedCount.Value = value;
    }
}