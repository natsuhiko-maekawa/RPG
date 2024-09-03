using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerImage")]
    public class PlayerImageListScriptableObject : BaseListScriptableObject<PlayerImageValueObject, PlayerImageCode>
    {
    }
}