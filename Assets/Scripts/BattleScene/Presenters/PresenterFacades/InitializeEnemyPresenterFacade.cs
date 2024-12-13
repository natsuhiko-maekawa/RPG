using BattleScene.Domain.Entities;
using BattleScene.Presenters.Presenters;

namespace BattleScene.Presenters.PresenterFacades
{
    public class InitializeEnemyPresenterFacade
    {
        private readonly OrderViewPresenter _orderView;

        public InitializeEnemyPresenterFacade(
            OrderViewPresenter orderView)
        {
            _orderView = orderView;
        }

        public void Initialize(CharacterEntity[] characterArray)
        {
            _orderView.Initialize(characterArray);
        }
    }
}