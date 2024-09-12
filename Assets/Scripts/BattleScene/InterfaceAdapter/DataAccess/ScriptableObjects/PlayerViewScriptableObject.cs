using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerView")]
    public class PlayerViewScriptableObject : BaseScriptableObject<PlayerViewDto, CharacterTypeCode>
    {
    }
}