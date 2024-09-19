using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class AilmentPropertyDto : IUnique<AilmentCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string ailmentCode;
        [SerializeField] private int turn;
        [SerializeField] private bool isSelfRecovery;
        public AilmentCode Key { get; private set; }
        public int Turn { get; private set; }
        public bool IsSelfRecovery { get; private set; }
        public Priority Priority { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<AilmentCode>(ailmentCode);
            Turn = turn;
            IsSelfRecovery = isSelfRecovery;
            Priority = Enum.TryParse<Priority>(ailmentCode, out var priority)
                ? priority
                : Priority.Lowest;
        }
    }
}