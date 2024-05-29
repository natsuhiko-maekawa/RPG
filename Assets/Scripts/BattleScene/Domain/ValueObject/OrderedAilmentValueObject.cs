using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class OrderedAilmentValueObject : IOrderedItem
    {
        public AilmentCode AilmentCode { get; }

        public OrderedAilmentValueObject(
            AilmentCode ailmentCode)
        {
            AilmentCode = ailmentCode;
        }
    }
}