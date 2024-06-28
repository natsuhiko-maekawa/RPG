using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;

namespace BattleScene.Framework.Resource
{
    public abstract class BaseListScriptableObject<TItem, TId> : ScriptableObject, ISerializationCallbackReceiver
        where TItem : IListScriptableObjectItem<TId>
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