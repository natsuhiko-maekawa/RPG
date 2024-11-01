using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class BodyPartViewDto : IUnique<BodyPartCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string bodyPartCode;
        [SerializeField] private string bodyPartName;
        public BodyPartCode Key { get; private set; }
        public string BodyPartName { get; private set; }
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<BodyPartCode>(bodyPartCode);
            BodyPartName = bodyPartName;
        }
    }
}