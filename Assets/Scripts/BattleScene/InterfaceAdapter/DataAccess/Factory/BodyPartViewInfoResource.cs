using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class BodyPartViewInfoResource : IResource<BodyPartViewInfoDto, BodyPartCode>
    {
        public BodyPartViewInfoDto Get(BodyPartCode bodyPartCode)
        {
            throw new System.NotImplementedException();
        }
    }
}