using BattleScene.DataAccesses.Dto;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjectResources
{
    public class BattlePropertyResource : MonoBehaviour, IResource<BattlePropertyDto>
    {
        [SerializeField] private BattlePropertyDto battlePropertyDto;

        public BattlePropertyDto Get() => battlePropertyDto;
    }
}