using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.Resource;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class PlayerPropertyFactory : IFactory<PlayerPropertyValueObject, CharacterTypeCode>
    {
        public PlayerPropertyValueObject Create(CharacterTypeCode id)
        {
            throw new System.NotImplementedException();
        }
    }
}