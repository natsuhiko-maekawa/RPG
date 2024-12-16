using System;
using BattleScene.Views.ViewModels;
using Cysharp.Text;
using TMPro;
using UnityEngine;
using Utility;

namespace BattleScene.Views.GameObjects
{
    public class Digit : MonoBehaviour
    {
        public string avoidMessage;
        public VertexGradient damageColor;
        public VertexGradient cureColor;
        public VertexGradient restoreColor;
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

        private void Reset()
        {
            avoidMessage = "AVOID!";
            damageColor = new VertexGradient(Color.yellow, Color.yellow, Color.red, Color.red);
            cureColor = new VertexGradient(Color.green, Color.green, Color.cyan, Color.cyan);
            restoreColor = new VertexGradient(Color.cyan, Color.cyan, Color.blue, Color.blue);
        }

        public void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(avoidMessage))
            {
                MyDebug.LogAssertion(ExceptionMessage.AvoidMessageIsEmpty);
            }
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

        public void ResetDigit()
        {
            _tmpText.SetText(Array.Empty<char>());
        }

        public void SetPosition(Vector3 vector3)
        {
            _originalPosition = transform.localPosition + vector3;
            transform.localPosition = _originalPosition;
        }

        private void OnEnable()
        {
            _animator.SetTrigger(ShowTrigger);
            _tmpText.enabled = true;
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