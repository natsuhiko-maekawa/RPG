using BattleScene.Views.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.Views
{
    public class FrameView : MonoBehaviour
    {
        [SerializeField] private Image frame;
        private Image _frame;

        private void Awake()
        {
            _frame = Instantiate(frame, transform);
            _frame.enabled = false;
        }

        public void StartAnimation(FrameViewModel model)
        {
            _frame.color = model.Color;
            _frame.enabled = true;
        }

        public void StopAnimation()
        {
            _frame.enabled = false;
        }
    }
}