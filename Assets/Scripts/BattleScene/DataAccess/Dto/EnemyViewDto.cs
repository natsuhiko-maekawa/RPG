using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class EnemyViewDto : IUnique<CharacterTypeCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string id;
        [SerializeField] private string enemyName;
        public CharacterTypeCode Key { get; private set; }
        public string EnemyName { get; private set; }
        public string EnemyImagePath { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<CharacterTypeCode>(id);
            EnemyName = enemyName;
            EnemyImagePath = $"{id}[{id}]";
        }
    }
}