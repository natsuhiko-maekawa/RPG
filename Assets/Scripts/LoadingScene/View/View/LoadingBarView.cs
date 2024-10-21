using UnityEngine;
using UnityEngine.UI;

namespace LoadingScene.View.View
{
    public class LoadingBarView : MonoBehaviour
    {
        [SerializeField] private Image progressBar;
        private Image _progressBar;

        private void Awake()
        {
            _progressBar = Instantiate(progressBar, transform);
        }

        public void StartAnimation(float progress)
        {
            _progressBar.rectTransform.localScale =
                new Vector3(progress, 1.0f, 1.0f);
        }
    }
}