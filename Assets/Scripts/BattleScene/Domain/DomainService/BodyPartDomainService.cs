using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.DomainService
{
    public class BodyPartDomainService
    {
        private readonly IRepository<BodyPartEntity, BodyPartId> _bodyPartRepository;

        public BodyPartDomainService(
            IRepository<BodyPartEntity, BodyPartId> bodyPartRepository)
        {
            _bodyPartRepository = bodyPartRepository;
        }

        public int Count(CharacterId characterId, BodyPartCode bodyPartCode)
        {
            var bodyPartEntity = _bodyPartRepository.Select()
                .FirstOrDefault(x => Equals(x.CharacterId, characterId) && x.BodyPartCode == bodyPartCode);
            return bodyPartEntity?.DestroyedPartCount() ?? 0;
        }

        public bool IsAvailable(CharacterId characterId, BodyPartCode bodyPartCode)
        {
            var bodyPartEntity = _bodyPartRepository.Select()
                .FirstOrDefault(x => Equals(x.CharacterId, characterId) && x.BodyPartCode == bodyPartCode);
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