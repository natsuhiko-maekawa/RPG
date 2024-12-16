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
        [SerializeField] private SkillComponentCode skillCode;
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
            skillCode = SkillComponentCode.All;
        }

        public SkillValueObject Select(CharacterEntity actor)
        {
            var characterTypeCode = actor.CharacterTypeCode;
            var skillCodeList = _characterPropertyFactory.Create(characterTypeCode).SkillCodeList;

            var skillList = skillCodeList
                .Select(_skillFactory.Create)
                .ToList();
            var specificSkillList = skillList
                .Where(IsSpecificSkillComponent)
                .ToList();
            var skill = specificSkillList.Count == 0
                ? _myRandom.Choice(skillList)
                : _myRandom.Choice(specificSkillList);
            return skill;
        }

        private bool IsSpecificSkillComponent(SkillValueObject skill)
        {
            var value = skillCode switch
            {
                SkillComponentCode.All => true,
                SkillComponentCode.Damage => skill.DamageList.Count != 0,
                SkillComponentCode.Ailment => skill.AilmentList.Count != 0,
                SkillComponentCode.Slip => skill.SlipList.Count != 0,
                SkillComponentCode.Destroy => skill.DestroyList.Count != 0,
                SkillComponentCode.Buff => skill.BuffList.Count != 0,
                SkillComponentCode.Restore => skill.RestoreList.Count != 0,
                _ => throw new ArgumentOutOfRangeException()
            };

            return value;
        }

        private enum SkillComponentCode
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