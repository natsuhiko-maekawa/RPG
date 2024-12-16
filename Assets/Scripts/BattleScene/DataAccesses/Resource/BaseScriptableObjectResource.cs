using System;
using System.Collections.Generic;
using BattleScene.DataAccesses.ScriptableObjects;
using UnityEngine;

namespace BattleScene.DataAccesses.Resource
{
    public abstract class BaseScriptableObjectResource<TScriptableObject, TItem, TId>
        : MonoBehaviour, IResource<TItem, TId>
        where TScriptableObject : BaseScriptableObject<TItem, TId>
        where TItem : IUnique<TId>
        where TId : notnull
    {
        [SerializeField] private TScriptableObject listScriptableObject;

        public TItem Get(TId id)
        {
            TItem item = default;
            var count = 0;

            foreach (var tmpItem in listScriptableObject.ItemList)
            {
                if (!tmpItem.Key.Equals(id)) continue;

                item = tmpItem;
                ++count;
            }

            if (item is null || count != 1) throw new InvalidOperationException(
                $"key {id.ToString()} not found in {typeof(TScriptableObject).Name}.");

            return item;
        }

        public void Get(List<TItem> itemList)
        {
            itemList.Clear();
            itemList.AddRange(listScriptableObject.ItemList);
        }
    }
}