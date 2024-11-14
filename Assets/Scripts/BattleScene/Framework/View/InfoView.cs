using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class InfoView : MonoBehaviour
    {
        [SerializeField] private Text infoText;
        private Text _text;

        private void Awake()
        {
            _text = Instantiate(infoText, transform);
        }

        public void StartAnimation(InfoViewModel info)
        {
            if (string.IsNullOrEmpty(info.Info)) return;
            _text.enabled = true;
            _text.text = info.Info;
        }

        public void StopAnimation()
        {
            _text.text = "";
            _text.enabled = false;
        }
    }
}