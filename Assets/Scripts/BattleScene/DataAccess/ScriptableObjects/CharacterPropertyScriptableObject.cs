using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CharacterProperty")]
    public class CharacterPropertyScriptableObject : BaseScriptableObject<CharacterPropertyDto, CharacterTypeCode>
    {
    }
}