using System;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Dto
{
    [Serializable]
    public class BattlePropertyDto : ISerializationCallbackReceiver
    {
        [SerializeField] private int slipDefaultTurn = 5;
        public int SlipDefaultTurn { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            SlipDefaultTurn = slipDefaultTurn;
        }
    }
}