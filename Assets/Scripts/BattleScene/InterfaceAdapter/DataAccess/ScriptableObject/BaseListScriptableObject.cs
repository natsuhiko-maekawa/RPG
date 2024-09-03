using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Interface;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.ScriptableObject
{
    public abstract class BaseListScriptableObject<TItem, TKey>
        : UnityEngine.ScriptableObject, ISerializationCallbackReceiver, IResource<TItem, TKey>
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

        public TItem Get(TKey key)
        {
            return ItemList.First(x => Equals(x.Key, key));
        }
    }
}