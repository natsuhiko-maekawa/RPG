using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class OrderedCharacterValueObject : IOrderedItem
    {
        public OrderedCharacterValueObject(
            CharacterId characterId)
        {
            CharacterId = characterId;
        }

        public CharacterId CharacterId { get; }
    }
}