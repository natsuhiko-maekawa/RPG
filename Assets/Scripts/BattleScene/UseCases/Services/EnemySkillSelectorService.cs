using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.Services
{
    public class EnemySkillSelectorService : IEnemySkillSelectorService
    {
        private readonly IFactory<CharacterPropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly IMyRandomService _myRandom;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;

        public EnemySkillSelectorService(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            IMyRandomService myRandom,
            IFactory<SkillValueObject, SkillCode> skillFactory)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _myRandom = myRandom;
            _skillFactory = skillFactory;
        }

        public SkillValueObject Select(CharacterEntity actor)
        {
            // TODO: 敵がスキルを選択する際、ランダムに選択する仮のアルゴリズムを実装している。
            var characterTypeCode = actor.CharacterTypeCode;
            var skillCodeList = _characterPropertyFactory.Create(characterTypeCode).SkillCodeList;
            var skillCode = _myRandom.Choice(skillCodeList);
            var skill = _skillFactory.Create(skillCode);
            return skill;
        }
    }
}