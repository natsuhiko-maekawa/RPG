using System.Collections.Generic;
using BattleScene.Domain.Entity;

namespace BattleScene.UseCases.IPresenter
{
    public interface IOrderViewPresenter
    {
        public void Start(IList<OrderedItemEntity> order);
    }
}