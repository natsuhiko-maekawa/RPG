using System.Threading.Tasks;
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

        public async Task Initialize(CharacterEntity[] characterArray)
        {
            await _orderView.Initialize(characterArray);
        }
    }
}