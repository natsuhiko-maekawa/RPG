using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class Damage : IPrimeSkill<DamageParameterValueObject, DamageValueObject>
    {
        private readonly DamageGeneratorService _damageGenerator;
        private readonly DamageRegistererService _damageRegisterer;
        private readonly BattleLoggerService _battleLogger;

        public Damage(
            DamageGeneratorService damageGenerator,
            DamageRegistererService damageRegisterer,
            BattleLoggerService battleLogger)
        {
            _damageGenerator = damageGenerator;
            _damageRegisterer = damageRegisterer;
            _battleLogger = battleLogger;
        }

        public IReadOnlyList<DamageValueObject> Commit(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DamageParameterValueObject> damageParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var damageList = _damageGenerator.Generate(
                skillCommon: skillCommon,
                damageParameterList: damageParameterList,
                targetIdList: targetIdList);
            _damageRegisterer.Register(damageList);
            _battleLogger.Log(damageList);

            return damageList;
        }
    }
}