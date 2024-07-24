using BattleScene.Domain.Entity;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Aggregate
{
    public class TechnicalPointAggregate : BaseEntity<TechnicalPointAggregate, CharacterId>
    {
        private PointValueObject _pointValueObject;

        public TechnicalPointAggregate(
            CharacterId id,
            int defaultTechnicalPoint)
        {
            Id = id;
            _pointValueObject = new PointValueObject(defaultTechnicalPoint);
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

        public int GetRestore(int technicalPoint)
        {
            return _pointValueObject.GetRestore(technicalPoint);
        }

        public void Add(int technicalPoint)
        {
            _pointValueObject = _pointValueObject.Add(technicalPoint);
        }

        public void Reduce(int technicalPoint)
        {
            _pointValueObject = _pointValueObject.Reduce(technicalPoint);
        }
    }
}