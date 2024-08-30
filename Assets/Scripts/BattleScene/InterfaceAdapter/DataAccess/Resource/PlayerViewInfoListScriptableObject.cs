using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerViewInfo")]
    public class PlayerViewInfoListScriptableObject : BaseListScriptableObject<PlayerViewInfoDto, CharacterTypeCode>
    {
    }
}