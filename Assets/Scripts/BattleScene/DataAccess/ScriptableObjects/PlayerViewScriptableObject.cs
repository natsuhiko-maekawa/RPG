using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerView")]
    public class PlayerViewScriptableObject : BaseScriptableObject<PlayerViewDto, CharacterTypeCode>
    {
    }
}