using System;
using System.Collections.Generic;
using BattleScene.Domain.Entities;

namespace Tests.BattleScene.UseCases.Service
{
    class OrderedItemEqualityComparator : IEqualityComparer<OrderedItemEntity>
    {
        public bool Equals(OrderedItemEntity x, OrderedItemEntity y)
        {
            if (x.Order != y.Order || x.ActorType != y.ActorType) return false;
            var value = x.ActorType switch
            {
                ActorType.Actor => EqualsCharacter(x, y),
                ActorType.Ailment => EqualsAilmentCode(x, y),
                ActorType.Slip => EqualsSlipCode(x, y),
                _ => throw new ArgumentOutOfRangeException()
            };

            return value;
        }

        public int GetHashCode(OrderedItemEntity obj)
        {
            var orderedItemInt = obj.ActorType switch
            {
                ActorType.Actor => GetCharacterIdHashCode(),
                ActorType.Ailment => GetAilmentCodeAsInt(),
                ActorType.Slip => GetSlipCodeAsInt(),
                _ => 0
            };

            var hashCode = (obj.Order, orderedItemInt).GetHashCode();
            return hashCode;

            int GetCharacterIdHashCode()
            {
                obj.TryGetActor(out var characterId);
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

        private static bool EqualsCharacter(OrderedItemEntity x, OrderedItemEntity y)
        {
            if (!x.TryGetActor(out var xCharacter)) return false;
            if (!y.TryGetActor(out var yCharacter)) return false;
            return xCharacter.Id == yCharacter.Id;
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