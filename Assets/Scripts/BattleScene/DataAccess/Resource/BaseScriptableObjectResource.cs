using System;
using System.Linq;
using BattleScene.DataAccess.ScriptableObjects;
using UnityEngine;

namespace BattleScene.DataAccess.Resource
{
    public abstract class BaseScriptableObjectResource<TScriptableObject, TItem, TId>
        : MonoBehaviour, IResource<TItem, TId>
        where TScriptableObject : BaseScriptableObject<TItem, TId>
        where TItem : IUnique<TId>
    {
        [SerializeField] private TScriptableObject listScriptableObject;

        public TItem Get(TId id)
        {
            TItem item;
            try
            {
                item = listScriptableObject.ItemList
                    .First(x => Equals(x.Key, id));
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(
                    $"key {id.ToString()} not found in {typeof(TScriptableObject).Name}.", e);
            }

            return item;
        }
    }
}