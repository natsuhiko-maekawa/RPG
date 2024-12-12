using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
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