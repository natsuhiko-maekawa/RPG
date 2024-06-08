using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.UseCase.Main
{
    public class Runner
    {
        private readonly BusinessLogicFactory _businessLogicFactory;
        private readonly IFrameRepository _frameRepository;
        private readonly OutputFactory _outputFactory;
        private int _frameNumber;

        public Runner(
            BusinessLogicFactory businessLogicFactory, 
            IFrameRepository frameRepository,
            OutputFactory outputFactory)
        {
            _businessLogicFactory = businessLogicFactory;
            _frameRepository = frameRepository;
            _outputFactory = outputFactory;
        }

        public void Start()
        {
            var frameNumber = new FrameNumber(_frameNumber);
            var businessLogicCodeList = ImmutableList.Create(
                BusinessLogicCode.InitializationLogic, 
                BusinessLogicCode.BattleStartLogic, 
                BusinessLogicCode.OrderDecisionLogic);
            var presenterCodeList = ImmutableList.Create(
                PresenterCode.AilmentView,
                // PresenterCode.AttackCount,
                // PresenterCode.Enemy,
                // PresenterCode.HitPointBar,
                // PresenterCode.TechnicalPointBar
                PresenterCode.Order
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
                _businessLogicFactory.Create(businessLogicCode).Execute(new FrameNumber(_frameNumber));

            foreach (var presenterCode in frame.PresenterCodeList)
            {
                _outputFactory.Create(presenterCode).Out();
            }
        }
    }
}