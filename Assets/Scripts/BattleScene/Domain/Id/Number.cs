using System;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Id
{
    public abstract class Number<T> : AbstractId<Number<T>, int>, IComparable<T> where T : Number<T>
    {
        protected override int Id { get; }

        protected Number(int number)
        {
            Id = number;
        }

        public int CompareTo(T other)
        {
            return Id - other.Id;
        }

        public int Get()
        {
            return Id;
        }
    }
}