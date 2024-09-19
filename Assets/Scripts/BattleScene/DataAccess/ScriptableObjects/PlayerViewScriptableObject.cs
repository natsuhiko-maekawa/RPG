using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerView")]
    public class PlayerViewScriptableObject : BaseScriptableObject<PlayerViewDto, CharacterTypeCode>
    {
    }
}