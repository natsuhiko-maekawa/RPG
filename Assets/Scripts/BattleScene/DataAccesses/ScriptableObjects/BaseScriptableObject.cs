using System.Collections.Generic;
using UnityEngine;

namespace BattleScene.DataAccesses.ScriptableObjects
{
    public abstract class BaseScriptableObject<TItem, TKey> : ScriptableObject, ISerializationCallbackReceiver
        where TItem : IUnique<TKey>
    {
        [SerializeField] private TItem[] itemList;
        public TItem[] ItemList { get; private set; }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            ItemList = itemList;
        }
    }
}