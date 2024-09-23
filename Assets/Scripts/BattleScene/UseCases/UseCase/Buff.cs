using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class Buff : IPrimeSkill<BuffParameterValueObject, BuffValueObject>
    {
        private readonly BuffGeneratorService _buffGenerator;
        private readonly BuffRegistererService _buffRegisterer;
        private readonly BattleLoggerService _battleLogger;

        public Buff(
            BuffGeneratorService buffGenerator,
            BuffRegistererService buffRegisterer,
            BattleLoggerService battleLogger)
        {
            _buffGenerator = buffGenerator;
            _buffRegisterer = buffRegisterer;
            _battleLogger = battleLogger;
        }

        public IReadOnlyList<BuffValueObject> Commit(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<BuffParameterValueObject> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var buffList = _buffGenerator.Generate(
                skillCommon: skillCommon,
                buffParameterList: primeSkillParameterList,
                targetIdList: targetIdList);
            _buffRegisterer.Register(buffList);
            _battleLogger.Log(buffList);
            return buffList;
        }
    }
}