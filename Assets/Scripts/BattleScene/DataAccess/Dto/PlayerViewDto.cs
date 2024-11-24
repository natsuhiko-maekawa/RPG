using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class PlayerViewDto : IUnique<CharacterTypeCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private string name;
        public CharacterTypeCode Key { get; private set; }
        public string Name { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<CharacterTypeCode>(key);
            Name = name;
        }
    }
}