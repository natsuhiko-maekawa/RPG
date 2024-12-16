using R3;

// ReSharper disable once CheckNamespace
namespace BattleScene.Domain.Entities
{
    public partial class CharacterEntity
    {
        private readonly ReactiveProperty<int> _reactiveCurrentHitPoint = new();
        public Observable<int> ReactiveCurrentHitPoint => _reactiveCurrentHitPoint;
        partial void CurrentHitPointOnChange(int value) => _reactiveCurrentHitPoint.Value = value;

        private readonly ReactiveProperty<int> _reactiveCurrentTechnicalPoint = new();
        public Observable<int> ReactiveCurrentTechnicalPoint => _reactiveCurrentTechnicalPoint;
        partial void CurrentTechnicalPointOnChange(int value) => _reactiveCurrentTechnicalPoint.Value = value;
    }
}