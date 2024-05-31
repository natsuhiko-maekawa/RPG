using System.Threading.Tasks;
using LoadingScene.InterfaceAdapter.Presenter.IView;
using UnityEngine;
using UnityEngine.UI;

namespace LoadingScene.UserInterface.View
{
    public class TipsView : MonoBehaviour, ITipsView
    {
        [SerializeField] private Text tipsText;
        private Text _text;

        private void Awake()
        {
            _text = Instantiate(tipsText, transform);
        }

        public Task StartAnimation(string tips)
        {
            _text.text = $"tips:\n{tips}";
            return Task.CompletedTask;
        }
    }
}