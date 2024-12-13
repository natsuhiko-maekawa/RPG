using BattleScene.Domain.Entity;
using BattleScene.InterfaceAdapter.Presenters;

namespace BattleScene.InterfaceAdapter.PresenterFacades
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