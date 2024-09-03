using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Interface;
using BattleScene.InterfaceAdapter.DataAccess.ScriptableObject;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public abstract class BaseListScriptableObjectResource<TListScriptableObject, TItem, TId>
        : MonoBehaviour, IResource<TItem, TId>
        where TListScriptableObject : BaseListScriptableObject<TItem, TId>
        where TItem : IUnique<TId>
    {
        [SerializeField] private TListScriptableObject listScriptableObject;
        
        public ImmutableList<TItem> Create()
        {
            return listScriptableObject.ItemList;
        }

        public TItem Get(TId id)
        {
            return listScriptableObject.ItemList
                .First(x => Equals(x.Key, id));
        }
    }
}