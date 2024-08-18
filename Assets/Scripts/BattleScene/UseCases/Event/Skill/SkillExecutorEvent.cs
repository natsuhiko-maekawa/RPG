using System;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.StateMachine;
using JetBrains.Annotations;

namespace BattleScene.UseCases.Event.Skill
{
    internal class SkillExecutorEvent : BaseEvent
    {
        private readonly SkillQueueFactory _skillQueueFactory;
        [CanBeNull] private SkillQueue _skillQueue;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRepository<SkillEntity, CharacterId> _skillRepository;
        private readonly AilmentSkillService _ailmentSkill;
        private StateCode _stateCode;

        public SkillExecutorEvent(
            SkillQueueFactory skillQueueFactory,
            OrderedItemsDomainService orderedItems,
            IRepository<SkillEntity, CharacterId> skillRepository)
        {
            _skillQueueFactory = skillQueueFactory;
            _orderedItems = orderedItems;
            _skillRepository = skillRepository;
        }

        public override void UseCase()
        {
            if (!_orderedItems.First().TryGetCharacterId(out var characterId)) throw new InvalidOperationException();
            var skill = _skillRepository.Select(characterId);
            _stateCode = ExecuteSkill(skill);
        }

        public override void Output()
        {
        }

        public override StateCode GetStateCode()
        {
            return _stateCode;
        }

        private StateCode ExecuteSkill(SkillEntity skill)
        {
            if (skill.SkillEffectList.IsEmpty) throw new NotImplementedException();
            var skillEffect = skill.DequeueSkillEffect();

            if (skillEffect is AilmentValueObject ailment)
            {
                _ailmentSkill.Execute(skill.Skill, ailment);
            }
            
            _skillRepository.Update(skill);
            throw new NotImplementedException();
        }
    }
}