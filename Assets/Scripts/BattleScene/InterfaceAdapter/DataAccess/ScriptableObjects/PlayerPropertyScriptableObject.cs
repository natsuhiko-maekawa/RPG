using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerProperty")]
    public class PlayerPropertyScriptableObject : BaseScriptableObject<PlayerPropertyDto, CharacterTypeCode>
    {
    }
}