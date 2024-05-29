using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;

namespace BattleScene.Domain.DomainService
{
    public class ResultDomainService
    {
        private readonly IResultRepository _resultRepository;
        private readonly ITurnRepository _turnRepository;

        public IDamageResult LastDamage()
        {
            var result = _resultRepository.Select()
                .Where(x => x.Result is IDamageResult)
                .OrderBy(x => x)
                .LastOrDefault(x => Equals(x.Turn, _turnRepository.Select().Get()));
            if (result == null) throw new InvalidOperationException();
            return (IDamageResult)result.Result;
        }

        public SkillCode LastSkillCode()
        {
            var result = _resultRepository.Select()
                .Where(x => x.Result is ISkillResult)
                .OrderBy(x => x)
                .LastOrDefault(x => Equals(x.Turn, _turnRepository.Select().Get()));
            if (result == null) throw new InvalidOperationException();
            var skillResult = (ISkillResult)result.Result;
            return skillResult.SkillCode;
        }

        public T Last<T>() where T : IResult
        {
            var result = _resultRepository.Select()
                .Where(x => x.Result is T)
                .OrderBy(x => x)
                .LastOrDefault(x => Equals(x.Turn, _turnRepository.Select().Get()));
            if (result == null) throw new InvalidOperationException();
            return (T)result.Result;
        }
        
        public bool TryGetLast<T>(out T result) where T : IResult
        {
            result = (T)_resultRepository.Select()
                .Where(x => x.Result is T)
                .OrderBy(x => x)
                .LastOrDefault(x => Equals(x.Turn, _turnRepository.Select().Get()))
                ?.Result;
            return result != null;
        }
    }
}