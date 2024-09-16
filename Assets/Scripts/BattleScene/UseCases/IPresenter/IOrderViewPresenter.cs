using System;
using System.Collections.Generic;
using BattleScene.Domain.Entity;

namespace BattleScene.UseCases.IPresenter
{
    [Obsolete]
    public interface IOrderViewPresenter
    {
        public void Start(IList<OrderedItemEntity> order);
    }
}