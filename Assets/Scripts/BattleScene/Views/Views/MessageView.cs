using BattleScene.Views.GameObjects;
using BattleScene.Views.IServices;
using BattleScene.Views.ViewModels;
using TMPro;
using UnityEngine;
using VContainer;

namespace BattleScene.Views.Views
{
    /// <summary>
    /// メッセージウィンドウのコンポーネント。<br/>
    /// </summary>
    // 以下のページおよび公式リファレンスを参考にTextMeshProを使用した。
    // https://nekojara.city/unity-textmesh-pro-typewriter-effect
    // また、ZStringと.maxVisibleCharactersプロパティを使用して、ゼロアロケーションの文字列表示を実現している。
    public class MessageView : MonoBehaviour
    {
        public int maxVisibleCharacters;
        public int maxCharacters = 100;
        private TMP_Text _tmpText;
        private Window _window;
        private Animator _animator;
        private IMyTextMeshProService _myTextMeshPro;
        private static readonly int ShowTrigger = Animator.StringToHash("Show");

        [Inject]
        private void Construct(
            IMyTextMeshProService myTextMeshPro)
        {
            _myTextMeshPro = myTextMeshPro;
        }

        private void Awake()
        {
            _tmpText = GetComponentInChildren<TMP_Text>();
            _animator = GetComponent<Animator>();
            _window = GetComponentInChildren<Window>();
            _window.enabled = true;
        }

        /// <summary>
        /// メッセージのアニメーション表示を開始するメソッド。<br/>
        /// 通常、一文字ずつメッセージを表示するが、MessageViewModel.NoWaitがtrueの場合、一度にすべての文字を表示する。
        /// </summary>
        /// <param name="model">ViewModel。</param>
        public void StartAnimation(MessageViewModel model)
        {
            _myTextMeshPro.SetTextZeroAlloc(_tmpText, model.Message);
            _tmpText.enabled = true;
            if (model.NoWait)
            {
                maxVisibleCharacters = maxCharacters;
            }
            else
            {
                _animator.SetTrigger(ShowTrigger);
            }
        }
    }
}