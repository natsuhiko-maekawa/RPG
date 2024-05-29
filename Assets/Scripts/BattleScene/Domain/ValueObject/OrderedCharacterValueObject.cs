using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class OrderedCharacterValueObject : IOrderedItem
    {
        public CharacterId CharacterId { get; }

        public OrderedCharacterValueObject(
            CharacterId characterId)
        {
            CharacterId = characterId;
        }
    }
}