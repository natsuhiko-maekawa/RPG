using System;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class EnemyViewDto : IUnique<CharacterTypeCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private string name;
        public CharacterTypeCode Key { get; private set; }
        public string Name { get; private set; }
        public string ImagePath { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<CharacterTypeCode>(key);
            Name = name;
            ImagePath = $"{key}[{key}]";
        }
    }
}