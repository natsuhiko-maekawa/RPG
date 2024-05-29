using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.MessageView;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface IMessageView
    {
        public Task StartAnimation(MessageViewDto dto);
        public void StopAnimation();
    }
}