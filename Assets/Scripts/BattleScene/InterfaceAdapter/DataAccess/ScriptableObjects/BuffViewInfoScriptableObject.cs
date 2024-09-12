using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuffViewInfo")]
    public class BuffViewInfoScriptableObject : BaseScriptableObject<BuffViewInfoDto, BuffCode>
    {
    }
}