using R3;

// ReSharper disable once CheckNamespace
namespace BattleScene.Domain.Entity
{
    public partial class BuffEntity
    {
        private readonly ReactiveProperty<float> _reactiveRate = new();
        public Observable<float> ReactiveRate => _reactiveRate;
        partial void RateOnChange(float value) => _reactiveRate.Value = value;
    }
}