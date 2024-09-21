using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageStateFactory
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly DamageGeneratorService _damageGenerator;
        private readonly DamageRegistererService _damageRegisterer;
        private readonly DamageMessageState _damageMessageState;

        public DamageStateFactory(
            BattleLoggerService battleLogger,
            DamageGeneratorService damageGenerator,
            DamageRegistererService damageRegisterer,
            DamageMessageState damageMessageState)
        {
            _battleLogger = battleLogger;
            _damageGenerator = damageGenerator;
            _damageRegisterer = damageRegisterer;
            _damageMessageState = damageMessageState;
        }

        public DamageState Create(
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter,
            IReadOnlyList<CharacterId> targetIdList) => new(
            battleLogger: _battleLogger,
            damageGenerator: _damageGenerator,
            damageMessageState: _damageMessageState,
            damageRegisterer: _damageRegisterer,
            skillCommon: skillCommon,
            damageParameter: damageParameter,
            targetIdList: targetIdList);
    }
}