using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CharacterProperty")]
    public class CharacterPropertyScriptableObject : BaseScriptableObject<CharacterPropertyDto, CharacterTypeCode>
    {
    }
}