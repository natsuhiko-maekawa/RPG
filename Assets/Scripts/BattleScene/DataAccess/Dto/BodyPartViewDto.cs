﻿using System;
using BattleScene.Domain.Code;
using UnityEngine;

namespace BattleScene.DataAccess.Dto
{
    [Serializable]
    public class BodyPartViewDto : IUnique<BodyPartCode>, ISerializationCallbackReceiver
    {
        [SerializeField] private string key;
        [SerializeField] private string name;
        public BodyPartCode Key { get; private set; }
        public string Name { get; private set; }
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Key = Enum.Parse<BodyPartCode>(key);
            Name = name;
        }
    }
}