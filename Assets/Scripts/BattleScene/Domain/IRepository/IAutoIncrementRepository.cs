﻿using BattleScene.Domain.Entity;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.IRepository
{
    public interface IAutoIncrementRepository<TEntity, TId> : IAutoIncrement<TId>, IRepository<TEntity, TId>
        where TEntity : BaseEntity<TEntity, TId>
        where TId : Number<TId>, new()
    {
    }
}