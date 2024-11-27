using System;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.IService;
using BattleScene.Framework.ViewModel;
using TMPro;
using UnityEngine;
using VContainer;

namespace BattleScene.Framework.View
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
            var window = GetComponentInChildren<Window>();
            window.Show();
        }

        /// <summary>
        /// メッセージのアニメーション表示を開始するメソッド。<br/>
        /// 通常、一文字ずつメッセージを表示するが、MessageViewModel.NoWaitがtrueの場合、一度にすべての文字を表示する。
        /// </summary>
        /// <param name="model">ViewModel。</param>
        public void StartAnimation(MessageViewModel model)
        {
            StopAnimation();
            _myTextMeshPro.SetTextZeroAlloc(ref _tmpText, model.Message);
            if (model.NoWait)
            {
                maxVisibleCharacters = maxCharacters;
            }
            else
            {
                _animator.SetTrigger(ShowTrigger);
            }

            _tmpText.enabled = true;
        }

        // QUESTION: Update()でmaxVisibleCharactersを設定すると稀に1フレームの間すべての文字が表示されることがあるため、
        // QUESTION: LateUpdate()で処理している。
        // QUESTION: LateUpdate()の使い方として正しいか自分では判断できない。
        private void LateUpdate()
        {
            // QUESTION: ここではif文でアニメーションが動く時だけプロパティを更新しているが、
            // QUESTION: パフォーマンスの観点から見て正しいコーディングと言えるか自分では判断できない。
            if (_tmpText.maxVisibleCharacters == 0 && maxVisibleCharacters == 0) return;
            _tmpText.maxVisibleCharacters = maxVisibleCharacters;
        }

        public void StopAnimation()
        {
            _tmpText.enabled = false;
            // QUESTION: SetText("")とすると空文字列の分アロケーションが発生してしまうためSetText(Array.Empty<char>())としているが、
            // QUESTION: この認識が正しいか自分では判断できない。
            _tmpText.SetText(Array.Empty<char>());
        }
    }
}