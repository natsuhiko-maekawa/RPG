using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class Ailment : IPrimeSkill<AilmentParameterValueObject, AilmentValueObject>
    {
        private readonly AilmentGeneratorService _ailmentGenerator;
        private readonly AilmentRegistererService _ailmentRegisterer;
        private readonly BattleLoggerService _battleLogger;

        public Ailment(
            AilmentGeneratorService ailmentGenerator,
            AilmentRegistererService ailmentRegisterer,
            BattleLoggerService battleLogger)
        {
            _ailmentGenerator = ailmentGenerator;
            _ailmentRegisterer = ailmentRegisterer;
            _battleLogger = battleLogger;
        }

        public IReadOnlyList<AilmentValueObject> Commit(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<AilmentParameterValueObject> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var ailmentList = _ailmentGenerator.Generate(
                skillCommon: skillCommon,
                primeSkillParameterList: primeSkillParameterList,
                targetIdList: targetIdList);
            _ailmentRegisterer.Register(ailmentList);
            _battleLogger.Log(ailmentList);
            return ailmentList;
        }
    }
}