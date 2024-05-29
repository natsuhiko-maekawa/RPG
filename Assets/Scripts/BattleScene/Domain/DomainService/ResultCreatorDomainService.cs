using BattleScene.Domain.Entity;
using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;

namespace BattleScene.Domain.DomainService
{
    public class ResultCreatorDomainService
    {
        private readonly ITurnRepository _turnRepository;
        private readonly ISequenceRepository _sequenceRepository;

        public ResultEntity Create(IResult result)
        {
            return new ResultEntity(
                turn: _turnRepository.Select().Get(),
                sequence: _sequenceRepository.Select().Get(),
                result: result);
        }
    }
}