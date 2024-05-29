using System.Threading.Tasks;

namespace LoadingScene.InterfaceAdapter.Presenter.IView
{
    public interface ITipsView
    {
        public Task StartAnimation(string tips);
    }
}