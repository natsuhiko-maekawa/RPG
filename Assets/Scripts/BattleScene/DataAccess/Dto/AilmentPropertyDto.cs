using System;
using BattleScene.Domain.Code;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class AilmentPropertyDto : IUnique<AilmentCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private int defaultTurn;
        [SerializeField] private bool isSelfRecovery;
        public AilmentCode Key { get; private set; }
        public int DefaultTurn { get; private set; }
        public bool IsSelfRecovery { get; private set; }
        public int? Priority { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<AilmentCode>(key);
            DefaultTurn = defaultTurn;
            IsSelfRecovery = isSelfRecovery;
            Priority = Enum.TryParse<Priority>(key, out var priority)
                ? (int?)priority
                : null;
        }
    }
}