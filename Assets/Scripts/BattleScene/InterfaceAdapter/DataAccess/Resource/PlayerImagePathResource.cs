using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class PlayerImagePathResource
        : BaseScriptableObjectResource<PlayerImageScriptableObject, PlayerImagePathDto, PlayerImageCode>
    {
    }
}