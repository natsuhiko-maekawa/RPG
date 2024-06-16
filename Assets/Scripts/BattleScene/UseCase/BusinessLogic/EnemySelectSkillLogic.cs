using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.BusinessLogic.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
using Utility.Interface;

namespace BattleScene.UseCase.BusinessLogic
{
    internal class EnemySelectSkillLogic : IBusinessLogic
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly OrderedItemsDomainService _orderItems;
        private readonly IRandomEx _randomEx;
        private readonly SkillCreatorService _skillCreatorService;
        private readonly ISkillRepository _skillRepository;
        private readonly TargetDomainService _target;
        private readonly ITargetRepository _targetRepository;

        public EnemySelectSkillLogic(
            ICharacterRepository characterRepository,
            OrderedItemsDomainService orderItems,
            IRandomEx randomEx,
            SkillCreatorService skillCreatorService,
            ISkillRepository skillRepository,
            TargetDomainService target,
            ITargetRepository targetRepository)
        {
            _characterRepository = characterRepository;
            _orderItems = orderItems;
            _randomEx = randomEx;
            _skillCreatorService = skillCreatorService;
            _skillRepository = skillRepository;
            _target = target;
            _targetRepository = targetRepository;
        }

        public void Execute(FrameNumber nextFrameNumber)
        {
            // TODO: 敵がスキルを選択する際、ランダムに選択する仮のアルゴリズムを実装している
            var characterId = _orderItems.FirstCharacterId();
            var skillCodeList = _characterRepository.Select(characterId).GetSkills();
            var skillCode = _randomEx.Choice(skillCodeList);
            var skill = _skillCreatorService.Create(characterId, skillCode);

            _skillRepository.Update(skill);

            var target = new TargetEntity(characterId, _target.Get(characterId, skill.AbstractSkill.GetRange()));
            _targetRepository.Update(target);
        }

        public EventCode NextEvent()
        {
            return EventCode.EnemyAttackEvent;
        }
    }
}