using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility.Interface;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.DomainService
{
    public class EnemySkillDomainService
    {
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRepository<OrderedItemEntity, OrderNumber> _orderItemsRepository;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        private readonly IRandomEx _randomEx;
        private readonly IFactory<AbstractSkill, SkillCode> _skillFactory;
        private readonly ISkillRepository _skillRepository;
        private readonly ITargetRepository _targetRepository;

        public EnemySkillDomainService(
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRepository<OrderedItemEntity, OrderNumber> orderItemsRepository,
            IRandomEx randomEx,
            IFactory<AbstractSkill, SkillCode> skillFactory,
            ISkillRepository skillRepository,
            ITargetRepository targetRepository)
        {
            _characterRepository = characterRepository;
            _orderItemsRepository = orderItemsRepository;
            _randomEx = randomEx;
            _skillFactory = skillFactory;
            _skillRepository = skillRepository;
            _targetRepository = targetRepository;
        }

        public void Set()
        {
            // TODO: 敵がスキルを選択する際、ランダムに選択する仮のアルゴリズムを実装している
            if (_orderItemsRepository.Select()
                .First()
                .TryGetCharacterId(out var characterId))
            {
                throw new InvalidOperationException();
            }
            
            var skillCodeList = _characterRepository.Select(characterId).GetSkills();
            var skillCode = _randomEx.Choice(skillCodeList);
            var skill = _skillFactory.Create(skillCode);
            var skillEntity = new SkillEntity(
                characterId: characterId,
                skillCode: skillCode,
                abstractSkill: skill);

            _skillRepository.Update(skillEntity);

            var targetIdList = GetTargetIdList(characterId, skillEntity.AbstractSkill.GetRange());
            var target = new TargetEntity(characterId, targetIdList);
            _targetRepository.Update(target);
        }
        
        private ImmutableList<CharacterId> GetTargetIdList(CharacterId characterId, Range range)
        {
            var targetList = range switch
            {
                Range.Random => GetTargetIdListRandom(characterId),
                Range.Oneself =>
                    _hitPointRepository.Select(characterId).IsSurvive()
                        ? ImmutableList<CharacterId>.Empty
                        : ImmutableList.Create(characterId),
                Range.Solo when !_characterRepository.Select(characterId).IsPlayer() =>
                    ImmutableList.Create(_characterRepository.Select().First(x => x.IsPlayer()).Id),
                _ => throw new NotImplementedException()
            };

            return targetList;
        }

        private ImmutableList<CharacterId> GetTargetIdListRandom(CharacterId characterId)
        {
            var targetList = _characterRepository.Select()
                .Select(x => x.Id)
                .Where(x => !Equals(x, characterId) && _hitPointRepository.Select(x).IsSurvive())
                .ToList();
            if (targetList.Count == 0) return ImmutableList<CharacterId>.Empty;
            return ImmutableList.Create(_randomEx.Choice(targetList));
        }
    }
}