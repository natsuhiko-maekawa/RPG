using System;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Id
{
    public class RandomId<T> : AbstractId<RandomId<T>, Guid>, IComparable<RandomId<T>> where T : IId
    {
        public override Guid Id { get; } = Guid.NewGuid();

        public int CompareTo(RandomId<T> other)
        {
            return GetHashCode() - other.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString("N");
        }
    }
}