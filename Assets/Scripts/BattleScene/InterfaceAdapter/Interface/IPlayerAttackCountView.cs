using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.PlayerAttackCountView;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IPlayerAttackCountView
    {
        public Task StartAnimation(PlayerAttackCountViewDto dto);
    }
}