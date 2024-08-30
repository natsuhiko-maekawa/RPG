using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
{
    [Serializable]
    public class EnemyViewInfoDto : ISerializationCallbackReceiver
    {
        [SerializeField] private string enemyTypeId;
        public string enemyName;
        public CharacterTypeCode EnemyTypeCode { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            EnemyTypeCode = Enum.Parse<CharacterTypeCode>(enemyTypeId);
        }
    }
}