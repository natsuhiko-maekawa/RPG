using System.Threading.Tasks;
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

        public Task StartAnimation(InfoViewDto dto)
        {
            if (string.IsNullOrEmpty(dto.Info)) return Task.CompletedTask;
            _text.enabled = true;
            _text.text = dto.Info;
            return Task.CompletedTask;
        }

        public void StopAnimation()
        {
            _text.text = "";
            _text.enabled = false;
        }
    }
}