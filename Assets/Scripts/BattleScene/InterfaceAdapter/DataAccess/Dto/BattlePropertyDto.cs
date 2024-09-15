using System;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Dto
{
    [Serializable]
    public class BattlePropertyDto : ISerializationCallbackReceiver
    {
        [SerializeField] private int slipDefaultTurn = 5;
        [SerializeField] private float slipDefaultDamageRate = 1.2f;
        public int SlipDefaultTurn { get; private set; }
        public float SlipDefaultDamageRate { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            SlipDefaultTurn = slipDefaultTurn;
            SlipDefaultDamageRate = slipDefaultDamageRate;
        }
    }
}