using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.UseCases.StateMachine;
using JetBrains.Annotations;

namespace BattleScene.UseCases.Event
{
    internal class SkillExecutorEvent : BaseEvent
    {
        private readonly SkillQueueFactory _skillQueueFactory;
        [CanBeNull] private SkillQueue _skillQueue;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRepository<SkillEntity, CharacterId> _skillRepository;
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
            _skillQueue ??= _skillQueueFactory.Create(skill.Skill);
            _stateCode = _skillQueue.Invoke();
        }

        public override void Output()
        {
        }

        public override StateCode GetStateCode()
        {
            return _stateCode;
        }
    }
}