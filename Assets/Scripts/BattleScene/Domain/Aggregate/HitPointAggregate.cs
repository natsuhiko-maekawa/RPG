using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Aggregate
{
    public class HitPointAggregate
    {
        private PointValueObject _pointValueObject;

        public HitPointAggregate(
            CharacterId characterId,
            int defaultHitPoint)
        {
            CharacterId = characterId;
            _pointValueObject = new PointValueObject(defaultHitPoint);
        }

        public CharacterId CharacterId { get; }

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
            return _pointValueObject.Get() <= 0;
        }
    }
}