using BattleScene.InterfaceAdapter.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObjects
{
    public class BattlePropertyResource : MonoBehaviour, IResource<BattlePropertyDto>
    {
        [SerializeField] private BattlePropertyDto battlePropertyDto;

        public BattlePropertyDto Get() => battlePropertyDto;
    }
}