using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using UnityEngine;

namespace BattleScene.Domain.ValueObject
{
    [Serializable]
    public class EnemyViewInfoValueObject : IUniqueItem<CharacterTypeId>, ISerializationCallbackReceiver
    {
        [SerializeField] private string id;
        [SerializeField] private string enemyName;
        [SerializeField] private string enemyImagePath;
        public CharacterTypeId Id { get; private set; }
        public string EnemyName { get; private set; }
        public string EnemyImagePath { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Id = Enum.Parse<CharacterTypeId>(id);
            EnemyName = enemyName;
            EnemyImagePath = enemyImagePath;
        }
    }
}