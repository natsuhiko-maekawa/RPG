using System;
using UnityEngine;

namespace BattleScene.DataAccesses.Dto
{
    [Serializable]
    public class BattlePropertyDto : ISerializationCallbackReceiver
    {
        [SerializeField] private int slipDefaultTurn = 5;
        [SerializeField] private float slipDefaultDamageRate = 1.2f;
        [SerializeField] private float isHitThreshold = 20.0f;
        [SerializeField] private float ailmentSuccessThreshold = 40.0f;
        [SerializeField] private int maxAgility = 256;
        [SerializeField] private int maxOrderCount = 14;
        [SerializeField] private int attackCountLimit = 25;
        public int SlipDefaultTurn { get; private set; }
        public float SlipDefaultDamageRate { get; private set; }
        public float IsHitThreshold { get; private set; }
        public float AilmentSuccessThreshold { get; private set; }
        public int MaxAgility { get; private set; }
        public int MaxOrderCount { get; private set; }
        public int AttackCountLimit { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            SlipDefaultTurn = slipDefaultTurn;
            SlipDefaultDamageRate = slipDefaultDamageRate;
            IsHitThreshold = isHitThreshold;
            AilmentSuccessThreshold = ailmentSuccessThreshold;
            MaxAgility = maxAgility;
            MaxOrderCount = maxOrderCount;
            AttackCountLimit = attackCountLimit;
        }
    }
}