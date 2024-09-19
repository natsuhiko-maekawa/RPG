using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerImagePath")]
    public class PlayerImagePathScriptableObject : BaseScriptableObject<PlayerImagePathDto, PlayerImageCode>
    {
    }
}