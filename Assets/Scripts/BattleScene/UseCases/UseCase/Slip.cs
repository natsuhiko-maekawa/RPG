using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class Slip : IPrimeSkill<SlipParameterValueObject, SlipValueObject>
    {
        private readonly SlipGeneratorService _slipGenerator;
        private readonly SlipRegistererService _slipRegisterer;
        private readonly BattleLoggerService _battleLogger;

        public Slip(
            SlipGeneratorService slipGenerator,
            SlipRegistererService slipRegisterer,
            BattleLoggerService battleLogger)
        {
            _slipGenerator = slipGenerator;
            _slipRegisterer = slipRegisterer;
            _battleLogger = battleLogger;
        }

        public IReadOnlyList<SlipValueObject> Commit(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<SlipParameterValueObject> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var slipList = _slipGenerator.Generate(
                skillCommon: skillCommon,
                slipParameterList: primeSkillParameterList,
                targetIdList: targetIdList);
            _slipRegisterer.Register(slipList);
            _battleLogger.Log(slipList);
            return slipList;
        }
    }
}