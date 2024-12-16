using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CharacterProperty")]
    public class CharacterPropertyScriptableObject : BaseScriptableObject<CharacterPropertyDto, CharacterTypeCode>
    {
    }
}