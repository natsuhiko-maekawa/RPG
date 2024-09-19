using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;

namespace BattleScene.DataAccess.ScriptableObjects
{
    public abstract class BaseScriptableObject<TItem, TKey> : ScriptableObject, ISerializationCallbackReceiver
        where TItem : IUnique<TKey>
    {
        [SerializeField] private List<TItem> itemList = new();
        public ImmutableList<TItem> ItemList { get; private set; }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            ItemList = itemList.ToImmutableList();
        }
    }
}