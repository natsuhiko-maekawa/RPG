using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using Utility.Interface;

namespace BattleScene.UseCases.UseCase
{
    public class EnemySkillSelector
    {
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderItems;
        private readonly IRandomEx _randomEx;

        public EnemySkillSelector(
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            OrderedItemsDomainService orderItems,
            IRandomEx randomEx)
        {
            _characterRepository = characterRepository;
            _orderItems = orderItems;
            _randomEx = randomEx;
        }

        public SkillCode Select()
        {
            // TODO: 敵がスキルを選択する際、ランダムに選択する仮のアルゴリズムを実装している
            _orderItems.First().TryGetCharacterId(out var characterId);
            var skillCodeList = _characterRepository.Select(characterId).GetSkills();
            return _randomEx.Choice(skillCodeList);
        }
    }
}