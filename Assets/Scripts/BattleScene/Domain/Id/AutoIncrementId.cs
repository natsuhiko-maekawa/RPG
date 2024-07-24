using System;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Id
{
    public class AutoIncrementId<T> : AbstractId<AutoIncrementId<T>, int>, IComparable<AutoIncrementId<T>> where T : IId
    {
        public override int Id { get; }

        public AutoIncrementId(int number)
        {
            Id = number;
        }

        public int CompareTo(AutoIncrementId<T> other)
        {
            return Id - other.Id;
        }
    }
}