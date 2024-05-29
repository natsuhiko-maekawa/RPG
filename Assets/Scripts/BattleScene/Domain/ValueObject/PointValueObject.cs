using System;

namespace BattleScene.Domain.ValueObject
{
    public class PointValueObject
    {
        public int MaxPoint { get; }
        public int CurrentPoint { get; }

        public PointValueObject(
            int defaultPoint)
        {
            MaxPoint = defaultPoint;
            CurrentPoint = defaultPoint;
        }

        public PointValueObject(
            int maxPoint,
            int currentPoint)
        {
            MaxPoint = maxPoint;
            CurrentPoint = currentPoint;
        }

        public int Get()
        {
            return CurrentPoint;
        }

        public int GetRestore(int point)
        {
            return Math.Min(point, MaxPoint - CurrentPoint);
        }

        public PointValueObject Add(int point)
        {
            var currentPoint = CurrentPoint + point <= MaxPoint ? CurrentPoint + point : MaxPoint;
            return new PointValueObject(MaxPoint, currentPoint);
        }

        public PointValueObject Reduce(int point)
        {
            var currentPoint = 0 <= CurrentPoint - point ? CurrentPoint - point : 0;
            return new PointValueObject(MaxPoint, currentPoint);
        }
    }
}