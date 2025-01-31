﻿using System;
using System.Collections.Generic;
using BattleScene.UseCases.IServices;
using UnityEngine;
using Utility;

namespace BattleScene.Debug.Services
{
    public class DebugRandomService : MonoBehaviour, IMyRandomService
    {
        [SerializeField] private ProbabilityDebugMode cantActionBecauseParalysis;
        [SerializeField] private ProbabilityDebugMode isHit;
        [SerializeField] private ProbabilityDebugMode isSuccess;

        private void Reset()
        {
            cantActionBecauseParalysis = ProbabilityDebugMode.Random;
            isHit = ProbabilityDebugMode.Random;
            isSuccess = ProbabilityDebugMode.Random;
        }

        public T Choice<T>(IEnumerable<T> options, string memberName = "")
        {
            return MyRandom.Choice(options);
        }

        public T Choice<T>(IEnumerable<T> options, long seed, string memberName = "")
        {
            return MyRandom.Choice(options, seed);
        }

        public bool Probability(float rate, string memberName = "")
        {
            var debugMode = memberName switch
            {
                "CantActionByParalysis" => cantActionBecauseParalysis,
                "BasicEvaluate" => isHit,
                "Pick" => isSuccess,
                _ => ProbabilityDebugMode.Random
            };

            var value = debugMode switch
            {
                ProbabilityDebugMode.Random => MyRandom.Probability(rate),
                ProbabilityDebugMode.True => true,
                ProbabilityDebugMode.False => false,
                _ => throw new ArgumentOutOfRangeException()
            };

            return value;
        }

        public int Range(int min, int max, string memberName = "")
        {
            return MyRandom.Range(min, max);
        }

        private enum ProbabilityDebugMode
        {
            Random,
            True,
            False
        }
    }
}