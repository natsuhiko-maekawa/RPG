﻿using System.Threading.Tasks;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class PlayerAttackCountView : MonoBehaviour
    {
        [SerializeField] private Image barFrontImage;
        private Image _image;

        private void Awake()
        {
            _image = Instantiate(barFrontImage, transform);
            _image.rectTransform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        }

        public Task StartAnimation(AttackCountViewModel model)
        {
            _image.rectTransform.localScale = new Vector3(model.Rate, 1.0f, 1.0f);
            return Task.CompletedTask;
        }
    }
}