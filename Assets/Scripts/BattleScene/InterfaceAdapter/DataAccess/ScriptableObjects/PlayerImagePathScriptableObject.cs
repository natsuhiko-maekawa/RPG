using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerImagePath")]
    public class PlayerImagePathScriptableObject : BaseScriptableObject<PlayerImagePathDto, PlayerImageCode>
    {
    }
}