﻿using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class EnemySkillSelectorService : IEnemySkillSelectorService
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly IFactory<CharacterPropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly IMyRandomService _myRandom;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;

        public EnemySkillSelectorService(
            ICollection<CharacterEntity, CharacterId> characterCollection,
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            IMyRandomService myRandom,
            IFactory<SkillValueObject, SkillCode> skillFactory)
        {
            _characterCollection = characterCollection;
            _characterPropertyFactory = characterPropertyFactory;
            _myRandom = myRandom;
            _skillFactory = skillFactory;
        }

        public SkillValueObject Select(CharacterId actorId)
        {
            // TODO: 敵がスキルを選択する際、ランダムに選択する仮のアルゴリズムを実装している
            var characterTypeCode = _characterCollection.Get(actorId).CharacterTypeCode;
            var skillCodeList = _characterPropertyFactory.Create(characterTypeCode).SkillCodeList;
            var skillCode = _myRandom.Choice(skillCodeList);
            var skill = _skillFactory.Create(skillCode);
            return skill;
        }
    }
}