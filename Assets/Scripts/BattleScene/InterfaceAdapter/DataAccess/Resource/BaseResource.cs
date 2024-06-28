using System.Collections.Immutable;
using System.Linq;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IResource;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Resource
{
    public abstract class BaseResource<TListScriptableObject, TItem, TId> : MonoBehaviour, IResource<TItem, TId>
        where TListScriptableObject : BaseListScriptableObject<TItem, TId>
        where TItem : IListScriptableObjectItem<TId>
    {
        [SerializeField] private TListScriptableObject listScriptableObject;
        
        public ImmutableList<TItem> Select()
        {
            return listScriptableObject.ItemList;
        }

        public TItem Select(TId id)
        {
            return listScriptableObject.ItemList
                .First(x => Equals(x.Id, id));
        }
    }
}