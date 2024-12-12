﻿using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using UnityEngine;
using VContainer;

namespace BattleScene.Debug.Service
{
    public class DebugEnemySkillSelectorService : MonoBehaviour, IEnemySkillSelectorService
    {
        [SerializeField] private PrimeSkillCode primeSkillCode;
        private IFactory<CharacterPropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private IFactory<SkillValueObject, SkillCode> _skillFactory;
        private IMyRandomService _myRandom;

        [Inject]
        public void Construct(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IMyRandomService myRandom)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _skillFactory = skillFactory;
            _myRandom = myRandom;
        }

        private void Reset()
        {
            primeSkillCode = PrimeSkillCode.All;
        }

        public SkillValueObject Select(CharacterEntity actor)
        {
            var characterTypeCode = actor.CharacterTypeCode;
            var skillCodeList = _characterPropertyFactory.Create(characterTypeCode).SkillCodeList;

            var skillList = skillCodeList
                .Select(_skillFactory.Create)
                .ToList();
            var specificPrimeSkillList = skillList
                .Where(IsSpecificPrimeSkill)
                .ToList();
            var skill = specificPrimeSkillList.Count == 0
                ? _myRandom.Choice(skillList)
                : _myRandom.Choice(specificPrimeSkillList);
            return skill;
        }

        private bool IsSpecificPrimeSkill(SkillValueObject skill)
        {
            var value = primeSkillCode switch
            {
                PrimeSkillCode.All => true,
                PrimeSkillCode.Damage => skill.DamageParameterList.Count != 0,
                PrimeSkillCode.Ailment => skill.AilmentParameterList.Count != 0,
                PrimeSkillCode.Slip => skill.SlipParameterList.Count != 0,
                PrimeSkillCode.Destroy => skill.DestroyedParameterList.Count != 0,
                PrimeSkillCode.Buff => skill.BuffParameterList.Count != 0,
                PrimeSkillCode.Restore => skill.RestoreParameterList.Count != 0,
                _ => throw new ArgumentOutOfRangeException()
            };

            return value;
        }

        private enum PrimeSkillCode
        {
            All,
            Damage,
            Ailment,
            Slip,
            Destroy,
            Buff,
            Restore
        }
    }
}