using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerViewInfoList")]
    public class PlayerViewInfoListScriptableObject : BaseListScriptableObject<PlayerViewInfoValueObject, PlayerImageCode>
    {
    }
}