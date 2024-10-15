using System;
using System.Collections.Generic;
using BattleScene.UseCases.IService;
using UnityEngine;
using VContainer;

namespace BattleScene.UseCases.Service.DebugService
{
    public class DebugRandomService : MonoBehaviour, IMyRandomService
    {
        [SerializeField] private ProbabilityDebugMode cantActionBecauseParalysis;
        [SerializeField] private ProbabilityDebugMode isHit;
        [SerializeField] private ProbabilityDebugMode isSuccess;
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
            var debugMode = memberName switch
            {
                "CantActionBecauseParalysis" => cantActionBecauseParalysis,
                "BasicEvaluate" => isHit,
                "Pick" => isSuccess,
                _ => ProbabilityDebugMode.Random
            };

            var value = debugMode switch
            {
                ProbabilityDebugMode.Random => _myRandom.Probability(rate),
                ProbabilityDebugMode.True => true,
                ProbabilityDebugMode.False => false,
                _ => throw new ArgumentOutOfRangeException()
            };

            return value;
        }

        public int Range(int min, int max, string memberName = "")
        {
            return _myRandom.Range(min, max);
        }

        private enum ProbabilityDebugMode
        {
            Random,
            True,
            False
        }
    }
}