using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.BusinessLogic.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;

namespace BattleScene.UseCase.BusinessLogic
{
    internal class InitializationLogic : IUseCase
    {
        private readonly CharacterCreatorService _characterCreator;
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly HitPointCreatorService _hitPointCreator;
        private readonly IPlayerPropertyFactory _playerPropertyFactory;
        private readonly ISelectorRepository _selectorRepository;
        private readonly ISkillSelectorRepository _skillSelectorRepository;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;

        public InitializationLogic(
            CharacterCreatorService characterCreator,
            ICharacterRepository characterRepository,
            CharactersDomainService characters,
            HitPointCreatorService hitPointCreator,
            IPlayerPropertyFactory playerPropertyFactory,
            ISelectorRepository selectorRepository,
            ISkillSelectorRepository skillSelectorRepository,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository)
        {
            _characterCreator = characterCreator;
            _characterRepository = characterRepository;
            _characters = characters;
            _hitPointCreator = hitPointCreator;
            _playerPropertyFactory = playerPropertyFactory;
            _selectorRepository = selectorRepository;
            _skillSelectorRepository = skillSelectorRepository;
            _hitPointRepository = hitPointRepository;
        }

        public void Execute()
        {
            var player = _characterCreator.CreatePlayer();
            _characterRepository.Update(player);

            var hitPoint = _hitPointCreator.Create(player);
            _hitPointRepository.Update(hitPoint);

            var actionSelectorId = new SelectorId(EventCode.SelectActionEvent);
            var actionSelector =
                new SelectorAggregate(actionSelectorId, Constant.ActionList.Count, Constant.ActionList.Count);
            _selectorRepository.Update(actionSelector);

            var skillSelectorId = new SkillSelectorId(EventCode.SelectSkillEvent);
            var skillNumber = _characterRepository.Select(_characters.GetPlayerId()).GetSkills().Count;
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