using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerView")]
    public class PlayerViewScriptableObject : BaseScriptableObject<PlayerViewDto, CharacterTypeCode>
    {
    }
}