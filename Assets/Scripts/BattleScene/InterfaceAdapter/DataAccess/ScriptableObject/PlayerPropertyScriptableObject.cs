using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObject
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerProperty")]
    public class PlayerPropertyScriptableObject : BaseListScriptableObject<PlayerPropertyDto, CharacterTypeCode>
    {
    }
}