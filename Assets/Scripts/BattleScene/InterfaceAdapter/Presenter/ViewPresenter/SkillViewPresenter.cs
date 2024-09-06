using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.InterfaceAdapter.Presenter.SelectSkillView;
using BattleScene.UseCases.Interface;
using BattleScene.UseCases.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.ViewPresenter
{
    public class SkillViewPresenter : IViewPresenter<SkillViewOutputData>
    {
        private readonly IResource<SkillViewInfoValueObject, SkillCode> _skillViewInfoFactory;
        private readonly IGridView _gridView;
        private readonly IVIew<SkillDto> _skillView;
        
        public void Start(SkillViewOutputData outputData)
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}