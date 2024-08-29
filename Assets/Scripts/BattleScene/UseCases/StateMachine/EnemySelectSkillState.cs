using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.UseCase;
using VContainer;

namespace BattleScene.UseCases.StateMachine
{
    internal class EnemySelectSkillState : AbstractState
    {
        private readonly EnemySelectSkill _enemySelectSkill;
        private readonly ISkillRepository _skillRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IObjectResolver _container;

        public EnemySelectSkillState(
            EnemySelectSkill enemySelectSkill,
            ISkillRepository skillRepository,
            OrderedItemsDomainService orderedItems,
            IObjectResolver container)
        {
            _enemySelectSkill = enemySelectSkill;
            _skillRepository = skillRepository;
            _orderedItems = orderedItems;
            _container = container;
        }

        public override void Start()
        {
            _enemySelectSkill.Execute();
            if (!_orderedItems.First().TryGetCharacterId(out var characterId))
            {
                throw new InvalidOperationException();
            }

            var skill = _skillRepository.Select(characterId);
            _container.Resolve<SkillStateFactory>().Create(skill.SkillCode);
        }
    }
}