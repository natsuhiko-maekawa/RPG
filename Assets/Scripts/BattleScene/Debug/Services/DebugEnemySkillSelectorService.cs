using System;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using UnityEngine;
using VContainer;

namespace BattleScene.Debug.Services
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
                PrimeSkillCode.Damage => skill.DamageList.Count != 0,
                PrimeSkillCode.Ailment => skill.AilmentList.Count != 0,
                PrimeSkillCode.Slip => skill.SlipList.Count != 0,
                PrimeSkillCode.Destroy => skill.DestroyList.Count != 0,
                PrimeSkillCode.Buff => skill.BuffList.Count != 0,
                PrimeSkillCode.Restore => skill.RestoreList.Count != 0,
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