using System;
using BattleScene.Framework.IServices;
using BattleScene.Framework.ViewModels;
using TMPro;
using UnityEngine;
using VContainer;

namespace BattleScene.Framework.Views
{
    public class InfoView : MonoBehaviour
    {
        private TMP_Text _tmpText;
        private IMyTextMeshProService _myTextMeshPro;

        [Inject]
        private void Construct(
            IMyTextMeshProService myTextMeshPro)
        {
            _myTextMeshPro = myTextMeshPro;
        }

        private void Awake()
        {
            _tmpText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void StartAnimation(InfoViewModel info)
        {
            if (info.Info.Length == 0) return;
            _myTextMeshPro.SetTextZeroAlloc(ref _tmpText, info.Info);
            _tmpText.enabled = true;
        }

        public void StopAnimation()
        {
            _tmpText.enabled = false;
            _tmpText.SetText(Array.Empty<char>());
        }
    }
}