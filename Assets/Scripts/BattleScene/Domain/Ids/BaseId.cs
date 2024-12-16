using System;

namespace BattleScene.Domain.Ids
{
    public abstract class BaseId : IComparable<BaseId>
    {
        private string Id { get; } = Guid.NewGuid().ToString("N");

        public override bool Equals(object? obj)
        {
            if (obj is null || GetType() != obj.GetType()) return false;
            var idObject = (BaseId)obj;
            return Equals(Id, idObject.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public int CompareTo(BaseId other)
        {
            return GetHashCode() - other.GetHashCode();
        }

        public override string ToString()
        {
            return Id;
        }

        public static bool operator ==(BaseId? a, BaseId? b)
        {
            // 少なくとも片方がnullのとき、排他的論理和を使用して両方がnullの場合にtrueを返している。
            // これは、aがnullのときEqualsメソッドが使用できないためである。
            // ただし、Object.Equalsメソッドを使えばnull同士の比較もできるため、以下のように書き換えることも可能。
            // if (a is null || b is null) return false;
            // return Equals(a, b);
            // QUESTION: どちらのコードの方が望ましいか。
            if (a is null || b is null) return !(a is null ^ b is null);
            return a.Equals(b);
        }

        public static bool operator !=(BaseId? a, BaseId? b) => !(a == b);
    }
}