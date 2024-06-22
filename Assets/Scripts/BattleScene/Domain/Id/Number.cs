using System;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Id
{
    public abstract class Number<T> : IId, IComparable<T> where T : Number<T>
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var number = (T)obj;
            return _number == number._number;
        }

        public override int GetHashCode()
        {
            return _number.GetHashCode();
        }
    }
}