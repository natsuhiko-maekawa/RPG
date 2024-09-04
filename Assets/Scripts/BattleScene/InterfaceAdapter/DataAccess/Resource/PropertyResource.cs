using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.DataAccess.ScriptableObject;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class PropertyResource
        : BaseListScriptableObjectResource<PropertyScriptableObject, PropertyDto, CharacterTypeCode>
    {
    }
}