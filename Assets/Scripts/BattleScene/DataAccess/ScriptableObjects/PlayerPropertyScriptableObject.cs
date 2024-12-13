using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerProperty")]
    public class PlayerPropertyScriptableObject : BaseScriptableObject<PlayerPropertyDto, CharacterTypeCode>
    {
    }
}