using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Aggregate
{
    public class TechnicalPointAggregate
    {
        public CharacterId CharacterId { get; }
        private PointValueObject _pointValueObject;

        public TechnicalPointAggregate(
            CharacterId characterId,
            int defaultTechnicalPoint)
        {
            CharacterId = characterId;
            _pointValueObject = new PointValueObject(defaultTechnicalPoint);
        }

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