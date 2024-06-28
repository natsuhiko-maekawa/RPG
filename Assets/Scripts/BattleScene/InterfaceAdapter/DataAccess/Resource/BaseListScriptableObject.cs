using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
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