using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerViewInfoList")]
    public class PlayerViewInfoListScriptableObject : BaseListScriptableObject<PlayerImageValueObject, PlayerImageCode>
    {
    }
}