using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace LoadingScene.View.View
{
    public class TipsView : MonoBehaviour
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