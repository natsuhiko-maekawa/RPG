using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
{
    [Serializable]
    public class EnemyViewInfoDto : IUnique<CharacterTypeCode>, ISerializationCallbackReceiver
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