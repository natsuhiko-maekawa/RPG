using System.Collections.Generic;
using BattleScene.UseCases.IService;
using UnityEngine;
using VContainer;

namespace BattleScene.UseCases.Service.DebugService
{
    public class DebugRandomService : MonoBehaviour, IMyRandomService
    {
        [SerializeField] private bool cantActionBecauseParalysis;
        [SerializeField] private bool isHit;
        private MyRandomService _myRandom;
        
        [Inject]
        public void Construct(MyRandomService myRandom)
        {
            _myRandom = myRandom;
        }
        
        public T Choice<T>(IEnumerable<T> options, string memberName = "")
        {
            return _myRandom.Choice(options);
        }

        public T Choice<T>(IEnumerable<T> options, long seed, string memberName = "")
        {
            return _myRandom.Choice(options, seed);
        }

        public bool Probability(float rate, string memberName = "")
        {
            return memberName switch
            {
                "CantActionBecauseParalysis" => cantActionBecauseParalysis,
                "BasicEvaluate" => isHit,
                _ => _myRandom.Probability(rate)
            };
        }

        public int Range(int min, int max, string memberName = "")
        {
            return _myRandom.Range(min, max);
        }
    }
}