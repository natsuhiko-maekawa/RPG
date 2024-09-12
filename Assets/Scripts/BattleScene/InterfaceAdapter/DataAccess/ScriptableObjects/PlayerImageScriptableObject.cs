using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerImage")]
    public class PlayerImageScriptableObject : BaseScriptableObject<PlayerImagePathDto, PlayerImageCode>
    {
    }
}