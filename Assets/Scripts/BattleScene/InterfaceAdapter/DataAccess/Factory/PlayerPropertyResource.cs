using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class PlayerPropertyResource : IResource<PlayerPropertyValueObject, CharacterTypeCode>
    {
        public PlayerPropertyValueObject Get(CharacterTypeCode id)
        {
            throw new System.NotImplementedException();
        }
    }
}