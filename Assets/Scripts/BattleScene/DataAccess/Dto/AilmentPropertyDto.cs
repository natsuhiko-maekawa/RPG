using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class AilmentPropertyDto : IUnique<AilmentCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private int turn;
        [SerializeField] private bool isSelfRecovery;
        public AilmentCode Key { get; private set; }
        public int Turn { get; private set; }
        public bool IsSelfRecovery { get; private set; }
        public int? Priority { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<AilmentCode>(key);
            Turn = turn;
            IsSelfRecovery = isSelfRecovery;
            Priority = Enum.TryParse<Priority>(key, out var priority)
                ? (int?)priority
                : null;
        }
    }
}