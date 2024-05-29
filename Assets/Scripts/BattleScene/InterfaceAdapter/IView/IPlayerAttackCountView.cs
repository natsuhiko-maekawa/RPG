using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.PlayerAttackCountView;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface IPlayerAttackCountView
    {
        public Task StartAnimation(PlayerAttackCountViewDto dto);
    }
}