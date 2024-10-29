using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using UnityEngine;
using VContainer;

namespace BattleScene.Debug.Service
{
    public class DebugEnemySkillSelectorService : MonoBehaviour, IEnemySkillSelectorService
    {
        [SerializeField] private PrimeSkillCode primeSkillCode;
        private ICollection<CharacterEntity, CharacterId> _characterCollection;
        private IFactory<CharacterPropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private IFactory<SkillValueObject, SkillCode> _skillFactory;
        private OrderedItemsDomainService _orderItems;
        private IMyRandomService _myRandom;

        [Inject]
        public void Construct(
            ICollection<CharacterEntity, CharacterId> characterCollection,
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            OrderedItemsDomainService orderItems,
            IMyRandomService myRandom)
        {
            _characterCollection = characterCollection;
            _characterPropertyFactory = characterPropertyFactory;
            _skillFactory = skillFactory;
            _orderItems = orderItems;
            _myRandom = myRandom;
        }

        [Obsolete]
        public SkillCode Select()
        {
            throw new NotImplementedException();
        }

        public SkillValueObject Select(CharacterId actorId)
        {
            var characterTypeCode = _characterCollection.Get(actorId).CharacterTypeCode;
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