using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public class EnemyViewInfoResource 
        : BaseScriptableObjectResource<EnemyViewInfoScriptableObject, EnemyViewInfoDto, CharacterTypeCode>
    {
    }
}