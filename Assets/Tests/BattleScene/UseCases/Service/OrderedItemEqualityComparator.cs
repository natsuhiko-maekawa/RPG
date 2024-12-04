using System;
using System.Collections.Generic;
using BattleScene.Domain.Entity;

namespace Tests.BattleScene.UseCases.Service
{
    class OrderedItemEqualityComparator : IEqualityComparer<OrderedItemEntity>
    {
        public bool Equals(OrderedItemEntity x, OrderedItemEntity y)
        {
            if (x.Order != y.Order || x.OrderedItemType != y.OrderedItemType) return false;
            var value = x.OrderedItemType switch
            {
                OrderedItemType.Character => EqualsCharacterId(x, y),
                OrderedItemType.Ailment => EqualsAilmentCode(x, y),
                OrderedItemType.Slip => EqualsSlipCode(x, y),
                _ => throw new ArgumentOutOfRangeException()
            };

            return value;
        }

        public int GetHashCode(OrderedItemEntity obj)
        {
            var orderedItemInt = obj.OrderedItemType switch
            {
                OrderedItemType.Character => GetCharacterIdHashCode(),
                OrderedItemType.Ailment => GetAilmentCodeAsInt(),
                OrderedItemType.Slip => GetSlipCodeAsInt(),
                _ => 0
            };

            var hashCode = (obj.Order, orderedItemInt).GetHashCode();
            return hashCode;

            int GetCharacterIdHashCode()
            {
                obj.TryGetCharacterId(out var characterId);
                return characterId?.GetHashCode() ?? 0;
            }

            int GetAilmentCodeAsInt()
            {
                obj.TryGetAilmentCode(out var ailmentCode);
                return (int)ailmentCode;
            }

            int GetSlipCodeAsInt()
            {
                obj.TryGetSlipCode(out var slipCode);
                return (int)slipCode;
            }
        }

        private static bool EqualsCharacterId(OrderedItemEntity x, OrderedItemEntity y)
        {
            if (!x.TryGetCharacterId(out var xCharacterId)) return false;
            if (!y.TryGetCharacterId(out var yCharacterId)) return false;
            return xCharacterId == yCharacterId;
        }

        private static bool EqualsAilmentCode(OrderedItemEntity x, OrderedItemEntity y)
        {
            if (!x.TryGetAilmentCode(out var xAilmentCode)) return false;
            if (!y.TryGetAilmentCode(out var yAilmentCode)) return false;
            return xAilmentCode == yAilmentCode;
        }

        private static bool EqualsSlipCode(OrderedItemEntity x, OrderedItemEntity y)
        {
            if (!x.TryGetSlipCode(out var xSlipCode)) return false;
            if (!y.TryGetSlipCode(out var ySlipCode)) return false;
            return xSlipCode == ySlipCode;
        }
    }
}