﻿using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class RestoreState : AbstractSkillState
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly RestoreGeneratorService _restoreGenerator;
        private readonly SkillEndState _skillEndState;
        private readonly SkillCommonValueObject _skillCommon;
        private readonly RestoreParameterValueObject _restoreParameter;

        public RestoreState(
            SkillCommonValueObject skillCommon,
            RestoreParameterValueObject restoreParameter,
            SkillEndState skillEndState,
            BattleLoggerService battleLogger,
            RestoreGeneratorService restoreGenerator)
        {
            _skillCommon = skillCommon;
            _restoreParameter = restoreParameter;
            _skillEndState = skillEndState;
            _battleLogger = battleLogger;
            _restoreGenerator = restoreGenerator;
        }

        public override void Select()
        {
            var restore = _restoreGenerator.Generate(
                skillCommon: _skillCommon,
                restoreParameter: _restoreParameter);
            _battleLogger.Log(restore);
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}