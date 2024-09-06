using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.MessageView;

namespace BattleScene.InterfaceAdapter.Interface
{
    public interface IMessageView
    {
        public Task StartAnimation(MessageViewDto dto);
        public void StopAnimation();
    }
}