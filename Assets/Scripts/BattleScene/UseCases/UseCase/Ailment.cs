using System.Collections.Generic;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Dto;
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

        public IReadOnlyList<AilmentValueObject> Commit(PrimeSkillParameterDto<AilmentParameterValueObject> primeSkillParameter)
        {
            var ailmentList = _ailmentGenerator.Generate(primeSkillParameter);
            _ailmentRegisterer.Register(ailmentList);
            _battleLogger.Log(ailmentList);
            return ailmentList;
        }
    }
}