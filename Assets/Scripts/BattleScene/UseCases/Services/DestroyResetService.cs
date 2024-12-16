using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.Services
{
    public class DestroyResetService : IDestroyResetService
    {
        private readonly IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> _bodyPartRepository;

        public DestroyResetService(
            IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> bodyPartRepository)
        {
            _bodyPartRepository = bodyPartRepository;
        }

        public void Reset(ILookup<CharacterId, BodyPartCode> bodyPartLookup)
        {
            foreach (var grouping in bodyPartLookup)
            {
                foreach (var bodyPartCode in grouping)
                {
                    _bodyPartRepository.Get((grouping.Key, bodyPartCode)).Recovered();
                }
            }
        }
    }
}