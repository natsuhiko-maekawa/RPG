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

namespace BattleScene.UseCases.Service.DebugService
{
    public class DebugEnemySkillSelectorService : MonoBehaviour, IEnemySkillSelectorService
    {
        [SerializeField] private bool isActive;
        [SerializeField] private PrimeSkillCode primeSkillCode;
        private IRepository<CharacterEntity, CharacterId> _characterRepository;
        private IFactory<PropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private IFactory<SkillValueObject, SkillCode> _skillFactory;
        private OrderedItemsDomainService _orderItems;
        private IMyRandomService _myRandom;

        [Inject]
        public void Construct(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IFactory<PropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            OrderedItemsDomainService orderItems,
            IMyRandomService myRandom)
        {
            _characterRepository = characterRepository;
            _characterPropertyFactory = characterPropertyFactory;
            _skillFactory = skillFactory;
            _orderItems = orderItems;
            _myRandom = myRandom;
        }

        public SkillCode Select()
        {
            _orderItems.First().TryGetCharacterId(out var characterId);
            var characterTypeCode = _characterRepository.Select(characterId).CharacterTypeCode;
            var skillCodeList = _characterPropertyFactory.Create(characterTypeCode).Skills;

            SkillCode skillCode;
            if (!isActive)
            {
                skillCode = _myRandom.Choice(skillCodeList);
                return skillCode;
            }
            
            skillCode = skillCodeList
                .FirstOrDefault(IsSpecificPrimeSkill);
            skillCode = skillCode == SkillCode.NoSkill
                ? skillCodeList.First()
                : skillCode;
            return skillCode;
        }

        private bool IsSpecificPrimeSkill(SkillCode skillCode)
        {
            var skill = _skillFactory.Create(skillCode);
            var value = primeSkillCode switch
            {
                PrimeSkillCode.Damage => !skill.DamageParameterList.IsEmpty,
                PrimeSkillCode.Ailment => !skill.AilmentParameterList.IsEmpty,
                PrimeSkillCode.Slip => !skill.SlipParameterList.IsEmpty,
                PrimeSkillCode.Destroy => !skill.DestroyedParameterList.IsEmpty,
                PrimeSkillCode.Buff => !skill.BuffParameterList.IsEmpty,
                PrimeSkillCode.Restore => !skill.RestoreParameterList.IsEmpty,
                _ => throw new ArgumentOutOfRangeException()
            };

            return value;
        }
    }
}