using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerImage")]
    public class PlayerImageListScriptableObject : BaseListScriptableObject<PlayerImagePathDto, PlayerImageCode>
    {
    }
}