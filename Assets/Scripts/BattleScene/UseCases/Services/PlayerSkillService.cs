using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.Services
{
    public class PlayerSkillService : IPlayerSkillService
    {
        private readonly IFactory<CharacterPropertyValueObject, CharacterTypeCode> _characterPropertyFactory;
        private readonly IFactory<PlayerPropertyValueObject, CharacterTypeCode> _playerPropertyFactory;

        public PlayerSkillService(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> characterPropertyFactory,
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _playerPropertyFactory = playerPropertyFactory;
        }

        public SkillCode[] Get() => _characterPropertyFactory.Create(CharacterTypeCode.Player).SkillCodeList;
        public SkillCode[] GetFatality() => _playerPropertyFactory.Create(CharacterTypeCode.Player).FatalitySkillList;
    }
}