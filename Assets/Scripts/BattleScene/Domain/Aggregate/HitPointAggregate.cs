using BattleScene.Domain.Entity;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Aggregate
{
    public class HitPointAggregate : BaseEntity<HitPointAggregate, CharacterId>
    {
        private PointValueObject _pointValueObject;

        public HitPointAggregate(
            CharacterId id,
            int defaultHitPoint)
        {
            Id = id;
            _pointValueObject = new PointValueObject(defaultHitPoint);
        }

        public override CharacterId Id { get; }

        public int GetMax()
        {
            return _pointValueObject.MaxPoint;
        }

        public int GetCurrent()
        {
            return _pointValueObject.CurrentPoint;
        }

        public int GetRestore(int hitPoint)
        {
            return _pointValueObject.GetRestore(hitPoint);
        }

        public void Add(int hitPoint)
        {
            _pointValueObject = _pointValueObject.Add(hitPoint);
        }

        public void Reduce(int hitPoint)
        {
            _pointValueObject = _pointValueObject.Reduce(hitPoint);
        }

        public bool IsSurvive()
        {
            return 0 < _pointValueObject.Get();
        }
    }
}