using System;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class BodyPartPropertyDto : IUnique<BodyPartCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private int count;
        public BodyPartCode Key { get; private set; }
        public int Count { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<BodyPartCode>(key);
            Count = count;
        }
    }
}