using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerImage")]
    public class PlayerImageScriptableObject : BaseScriptableObject<PlayerImageDto, PlayerImageCode>
    {
    }
}