using BattleScene.DataAccess.Dto;
using UnityEngine;

namespace BattleScene.DataAccess.Resource
{
    public class BattlePropertyResource : MonoBehaviour, IResource<BattlePropertyDto>
    {
        [SerializeField] private BattlePropertyDto battlePropertyDto;

        public BattlePropertyDto Get() => battlePropertyDto;
    }
}