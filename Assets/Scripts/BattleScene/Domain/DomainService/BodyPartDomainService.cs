using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.DomainService
{
    public class BodyPartDomainService
    {
        private readonly IBodyPartRepository _bodyPartRepository;

        public BodyPartDomainService(
            IBodyPartRepository bodyPartRepository)
        {
            _bodyPartRepository = bodyPartRepository;
        }

        public int Count(CharacterId characterId, BodyPartCode bodyPartCode)
        {
            var bodyPartEntity = _bodyPartRepository.Select(characterId, bodyPartCode);
            return bodyPartEntity?.DestroyedPartCount() ?? 0;
        }

        public bool IsAvailable(CharacterId characterId, BodyPartCode bodyPartCode)
        {
            var bodyPartEntity = _bodyPartRepository.Select(characterId, bodyPartCode);
            return bodyPartEntity?.IsAvailable() ?? true;
        }

        public bool IsAvailable(CharacterId characterId, IList<BodyPartCode> bodyPartCodeList)
        {
            if (bodyPartCodeList.Count == 0) return true;
            return bodyPartCodeList
                .Select(x => IsAvailable(characterId, x))
                .Aggregate((x, y) => x && y);
        }
    }
}