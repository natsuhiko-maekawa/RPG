using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.Interface;
using BattleScene.InterfaceAdapter.DataAccess.Resource;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class BaseListScriptableObjectFactory<TListScriptableObject, TItem, TId>
        : MonoBehaviour, IFactory<TItem, TId>
        where TListScriptableObject : BaseListScriptableObject<TItem, TId>
        where TItem : IUniqueItem<TId>
    {
        [SerializeField] private TListScriptableObject listScriptableObject;
        
        public ImmutableList<TItem> Create()
        {
            return listScriptableObject.ItemList;
        }

        public TItem Create(TId id)
        {
            return listScriptableObject.ItemList
                .First(x => Equals(x.Id, id));
        }
    }
}