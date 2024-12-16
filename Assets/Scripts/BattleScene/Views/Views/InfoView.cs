using System;
using BattleScene.Views.IServices;
using BattleScene.Views.ViewModels;
using TMPro;
using UnityEngine;
using VContainer;

namespace BattleScene.Views.Views
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
            _myTextMeshPro.SetTextZeroAlloc(_tmpText, info.Info);
            _tmpText.enabled = true;
        }

        public void StopAnimation()
        {
            _tmpText.enabled = false;
            _tmpText.SetText(Array.Empty<char>());
        }
    }
}