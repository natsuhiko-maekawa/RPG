using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service.DebugService
{
    public class EnemySpecificSkillSelectorService : IEnemySkillSelectorService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly OrderedItemsDomainService _orderItems;

        public EnemySpecificSkillSelectorService(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IFactory<PropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            OrderedItemsDomainService orderItems)
        {
            _characterRepository = characterRepository;
            _characterPropertyFactory = characterPropertyFactory;
            _skillFactory = skillFactory;
            _orderItems = orderItems;
        }

        public SkillCode Select()
        {
            _orderItems.First().TryGetCharacterId(out var characterId);
            var characterTypeCode = _characterRepository.Select(characterId).CharacterTypeCode;
            var skillCodeList = _characterPropertyFactory.Create(characterTypeCode).Skills;
            var skillCode = skillCodeList
                .FirstOrDefault(x => !_skillFactory.Create(x).DestroyedParameterList.IsEmpty);
            skillCode = skillCode == SkillCode.NoSkill
                ? skillCodeList.First()
                : skillCode;
            return skillCode;
        }
    }
}