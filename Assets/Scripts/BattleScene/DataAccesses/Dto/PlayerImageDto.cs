using System;
using BattleScene.Domain.Codes;
using UnityEngine;

namespace BattleScene.DataAccesses.Dto
{
    [Serializable]
    public class PlayerImageDto : IUnique<PlayerImageCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private string path;
        public PlayerImageCode Key { get; private set; }
        public string Path { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<PlayerImageCode>(key);
            Path = $"{path}[{path}]";
        }
    }
}