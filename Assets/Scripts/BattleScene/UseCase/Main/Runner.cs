using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.UseCase.Main
{
    public class Runner
    {
        private readonly UseCaseFactory _useCaseFactory;
        private readonly IFrameRepository _frameRepository;
        private readonly OutputFactory _outputFactory;
        private int _frameNumber;

        public Runner(
            UseCaseFactory useCaseFactory, 
            IFrameRepository frameRepository,
            OutputFactory outputFactory)
        {
            _useCaseFactory = useCaseFactory;
            _frameRepository = frameRepository;
            _outputFactory = outputFactory;
        }

        public void Start()
        {
            var frameNumber = new FrameNumber(_frameNumber);
            var businessLogicCodeList = ImmutableList.Create(
                UseCaseCode.InitializationLogic, 
                UseCaseCode.BattleStartLogic, 
                UseCaseCode.OrderDecisionLogic);
            var presenterCodeList = ImmutableList.Create(
                OutputCode.AilmentView,
                // PresenterCode.AttackCount,
                // PresenterCode.Enemy,
                // PresenterCode.HitPointBar,
                // PresenterCode.TechnicalPointBar
                OutputCode.Order
            );
            var frame = new FrameEntity(
                frameNumber: frameNumber,
                businessLogicCodeList: businessLogicCodeList,
                presenterCodeList: presenterCodeList);
            _frameRepository.Update(frame);
        }

        public void Update()
        {
            var frame = _frameRepository.Select(new FrameNumber(_frameNumber));
            _frameNumber++;
            _frameRepository.Update(new FrameEntity(new FrameNumber(_frameNumber)));
            foreach (var businessLogicCode in frame.BusinessLogicCodeList)
                _useCaseFactory.Create(businessLogicCode).Execute();

            foreach (var presenterCode in frame.PresenterCodeList)
            {
                _outputFactory.Create(presenterCode).Out();
            }
        }
    }
}