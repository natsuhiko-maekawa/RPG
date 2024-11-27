using System;
using BattleScene.Framework.ViewModel;
using Cysharp.Text;
using TMPro;
using UnityEngine;

namespace BattleScene.Framework.GameObjects
{
    public class Digit : MonoBehaviour
    {
        public float x;
        [SerializeField] private string avoidMessage = "AVOID!";
        [SerializeField] private VertexGradient damageColor = new(Color.yellow, Color.yellow, Color.red, Color.red);
        [SerializeField] private VertexGradient cureColor = new(Color.green, Color.green, Color.cyan, Color.cyan);
        [SerializeField] private VertexGradient restoreColor = new(Color.cyan, Color.cyan, Color.blue, Color.blue);
        private TMP_Text _tmpText;
        private Animator _animator;
        private Vector3 _originalPosition;
        private static readonly int ShowTrigger = Animator.StringToHash("Show");

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
            _tmpText.enabled = false;
            _animator = GetComponent<Animator>();
            enabled = false;
        }

        public void SetAvoid()
        {
            _tmpText.SetText(avoidMessage);
            _tmpText.enableVertexGradient = false;
        }

        public void SetDigit(int value, DigitType digitType)
        {
            using (var stringBuilder = ZString.CreateStringBuilder())
            {
                stringBuilder.Append(value);
                _tmpText.SetText(stringBuilder);
            }

            _tmpText.enableVertexGradient = true;
            _tmpText.colorGradient = digitType switch
            {
                DigitType.Damage => damageColor,
                DigitType.Cure => cureColor,
                DigitType.Restore => restoreColor,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void OnEnable()
        {
            _animator.SetTrigger(ShowTrigger);
            _tmpText.enabled = true;
        }

        private void Update()
        {
            transform.localPosition = _originalPosition + new Vector3(x, 0, 0);
        }

        public void OnAnimationExit()
        {
            enabled = false;
        }

        private void OnDisable()
        {
            _tmpText.enabled = false;
        }
    }
}