using System;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.IService;
using BattleScene.Framework.ViewModel;
using TMPro;
using UnityEngine;
using VContainer;

namespace BattleScene.Framework.View
{
    // 以下のページを参考にTextMeshProを使用するよう修正する
    // https://nekojara.city/unity-textmesh-pro-typewriter-effect
    // .maxVisibleCharactersプロパティを使用してゼロアロケーションの文字表示を実現すること
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

        public void StartAnimation(MessageViewDto dto)
        {
            _myTextMeshPro.SetTextZeroAlloc(ref _tmpText, dto.Message);
            _tmpText.enabled = true;
            if (dto.NoWait)
            {
                maxVisibleCharacters = maxCharacters;
            }
            else
            {
                _animator.SetTrigger(ShowTrigger);
            }
        }

        private void Update()
        {
            _tmpText.maxVisibleCharacters = maxVisibleCharacters;
        }

        public void StopAnimation()
        {
            _tmpText.enabled = false;
            // SetText("")とすると空文字列の分アロケーションが発生してしまうためSetText(Array.Empty<char>())としているが、
            // この認識は間違っているかもしれない。
            _tmpText.SetText(Array.Empty<char>());
        }
    }
}