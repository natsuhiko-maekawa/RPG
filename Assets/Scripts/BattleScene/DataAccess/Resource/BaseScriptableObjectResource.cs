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
            return listScriptableObject.ItemList
                .First(x => Equals(x.Key, id));
        }
    }
}