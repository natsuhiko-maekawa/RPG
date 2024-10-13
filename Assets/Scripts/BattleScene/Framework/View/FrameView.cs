using System.Threading.Tasks;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
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

        public Task StartAnimation(FrameViewDto dto)
        {
            _frame.color = dto.Color;
            _frame.enabled = true;
            return Task.CompletedTask;
        }

        public void StopAnimation()
        {
            _frame.enabled = false;
        }
    }
}