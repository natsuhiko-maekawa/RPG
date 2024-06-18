﻿using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> 
        where TEntity : BaseEntity<TEntity, TId> 
        where TId : class, IId
    {
        private readonly HashSet<TEntity> _entitySet = new();

        public TEntity Select(TId id)
        {
            return _entitySet
                .First(x => x.Id == id);
        }

        public void Update(TEntity entity)
        {
            _entitySet.Update(entity);
        }

        public void Update(IList<TEntity> entityList)
        {
            foreach (var entity in entityList)
            {
                Update(entity);
            }
        }

        public void Delete(TId id)
        {
            _entitySet.RemoveWhere(x => x.Id == id);
        }

        public void Delete(IList<TId> idList)
        {
            foreach (var id in idList)
            {
                Delete(id);
            }
        }
    }
}