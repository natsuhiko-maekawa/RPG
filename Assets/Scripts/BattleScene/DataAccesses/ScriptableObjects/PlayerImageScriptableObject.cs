using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerImage")]
    public class PlayerImageScriptableObject : BaseScriptableObject<PlayerImageDto, PlayerImageCode>
    {
    }
}