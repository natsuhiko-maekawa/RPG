using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    public class EnemySkillSelectorService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IFactory<PropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly OrderedItemsDomainService _orderItems;
        private readonly IRandomEx _randomEx;

        public EnemySkillSelectorService(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IFactory<PropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            OrderedItemsDomainService orderItems,
            IRandomEx randomEx)
        {
            _characterRepository = characterRepository;
            _characterPropertyFactory = characterPropertyFactory;
            _orderItems = orderItems;
            _randomEx = randomEx;
        }

        public SkillCode Select()
        {
            // TODO: 敵がスキルを選択する際、ランダムに選択する仮のアルゴリズムを実装している
            _orderItems.First().TryGetCharacterId(out var characterId);
            var characterTypeCode = _characterRepository.Select(characterId).CharacterTypeCode;
            var skillCodeList = _characterPropertyFactory.Create(characterTypeCode).Skills;
            return _randomEx.Choice(skillCodeList);
        }
    }
}