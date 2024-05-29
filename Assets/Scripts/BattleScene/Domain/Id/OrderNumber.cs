using System;

namespace BattleScene.Domain.Id
{
    public class OrderNumber : IComparable<OrderNumber>
    {
        private readonly int _id;

        public OrderNumber(int id)
        {
            if (id is < 0 or > Constant.MaxOrderNumber) throw new ArgumentOutOfRangeException();
            _id = id;
        }

        public static OrderNumber First()
        {
            return new OrderNumber(0);
        }

        public int CompareTo(OrderNumber other)
        {
            return _id - other._id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var orderId = (OrderNumber)obj;
            return _id == orderId._id;
        }
        
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}