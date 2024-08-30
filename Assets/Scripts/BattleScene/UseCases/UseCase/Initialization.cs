using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.UseCase.Interface;

namespace BattleScene.UseCases.UseCase
{
    internal class Initialization : IUseCase
    {
        private readonly PlayerDomainService _player;
        private readonly IPlayerPropertyFactory _playerPropertyFactory;
        private readonly ISelectorRepository _selectorRepository;
        private readonly ISkillSelectorRepository _skillSelectorRepository;

        public Initialization(
            PlayerDomainService player,
            IPlayerPropertyFactory playerPropertyFactory,
            ISelectorRepository selectorRepository,
            ISkillSelectorRepository skillSelectorRepository)
        {
            _player = player;
            _playerPropertyFactory = playerPropertyFactory;
            _selectorRepository = selectorRepository;
            _skillSelectorRepository = skillSelectorRepository;
        }

        public void Execute()
        {
            _player.Add();

            var actionSelectorId = new SelectorId(EventCode.SelectActionEvent);
            var actionSelector =
                new SelectorAggregate(actionSelectorId, Constant.ActionList.Count, Constant.ActionList.Count);
            _selectorRepository.Update(actionSelector);

            var skillSelectorId = new SkillSelectorId(EventCode.SelectSkillEvent);
            var skillNumber = _player.Get().GetSkills().Count;
            var skillSelector =
                new SkillSelectorAggregate(skillSelectorId, Constant.SelectSkillSlotNumber, skillNumber);
            _skillSelectorRepository.Update(skillSelector);

            var fatalitySkillSelectorId = new SkillSelectorId(EventCode.SelectFatalitySkillEvent);
            var fatalitySkillNumber = _playerPropertyFactory.Get().FatalitySkills.Count;
            var fatalitySkillSelector
                = new SkillSelectorAggregate(fatalitySkillSelectorId, Constant.SelectSkillSlotNumber,
                    fatalitySkillNumber);
            _skillSelectorRepository.Update(fatalitySkillSelector);
        }
    }
}