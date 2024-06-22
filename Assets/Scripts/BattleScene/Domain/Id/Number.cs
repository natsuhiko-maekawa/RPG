using System;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Id
{
    public abstract class Number<T> : AbstractId<Number<T>, int>, IComparable<T> where T : Number<T>
    {
        private readonly int _number;

        protected Number(int number)
        {
            _number = number;
        }

        public int CompareTo(T other)
        {
            return _number - other._number;
        }

        public int Get()
        {
            return _number;
        }
    }
}