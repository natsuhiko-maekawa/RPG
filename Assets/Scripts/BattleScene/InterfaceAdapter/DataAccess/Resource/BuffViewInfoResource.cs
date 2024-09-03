using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.DataAccess.ScriptableObject;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class BuffViewInfoResource 
        : BaseListScriptableObjectResource<BuffViewInfoScriptableObject, BuffViewInfoDto, BuffCode>
    {
    }
}