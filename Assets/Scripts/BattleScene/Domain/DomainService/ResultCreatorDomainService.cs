using BattleScene.Domain.Entity;
using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;

namespace BattleScene.Domain.DomainService
{
    public class ResultCreatorDomainService
    {
        private readonly ISequenceRepository _sequenceRepository;
        private readonly ITurnRepository _turnRepository;

        public ResultEntity Create(IResult result)
        {
            return new ResultEntity(
                _turnRepository.Select().Get(),
                _sequenceRepository.Select().Get(),
                result);
        }
    }
}