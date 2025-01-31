using System;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DomainServices;
using BattleScene.Presenters.Services.Replacements.Interfaces;
using Utility;

namespace BattleScene.Presenters.Services.Replacements
{
    public class DestroyedReplacementService : IReplacementService
    {
        public string Replacement { get; } = "[destroyed]";
        private readonly BattleLogDomainService _battleLog;
        private readonly IResource<BodyPartViewDto, BodyPartCode> _bodyPartViewResource;

        public DestroyedReplacementService(
            BattleLogDomainService battleLog,
            IResource<BodyPartViewDto, BodyPartCode> bodyPartViewResource)
        {
            _battleLog = battleLog;
            _bodyPartViewResource = bodyPartViewResource;
        }

        public bool IsMatch(string value) => value == Replacement;

        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var destroyedPart = _battleLog.GetLast().DestroyedPart;
            MyDebug.Assert(destroyedPart != BodyPartCode.NoBodyPart);
            if (destroyedPart == BodyPartCode.NoBodyPart) return string.Empty;
            var ailmentName = _bodyPartViewResource.Get(destroyedPart).Name;
            return ailmentName;
        }
    }
}