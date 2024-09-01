using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.UseCases.UseCase
{
    public class EnemySkillSelector
    {
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderItems;
        private readonly IRandomEx _randomEx;
        private readonly IFactory<SkillValueObject, SkillCode> _skillCreatorService;
        private readonly ISkillRepository _skillRepository;
        private readonly TargetDomainService _target;
        private readonly ITargetRepository _targetRepository;

        public EnemySkillSelector(
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            OrderedItemsDomainService orderItems,
            IRandomEx randomEx,
            IFactory<SkillValueObject, SkillCode> skillCreatorService,
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

        public SkillCode Select()
        {
            // TODO: 敵がスキルを選択する際、ランダムに選択する仮のアルゴリズムを実装している
            _orderItems.First().TryGetCharacterId(out var characterId);
            var skillCodeList = _characterRepository.Select(characterId).GetSkills();
            var skillCode = _randomEx.Choice(skillCodeList);
            var skill = _skillCreatorService.Create(skillCode);
            // TODO:　以下の一行を削除すること
            _skillRepository.Update(new SkillEntity(characterId, skill));

            var target = new TargetEntity(characterId, _target.Get(characterId, skill.Range));
            _targetRepository.Update(target);
            
            return skillCode;
        }
    }
}