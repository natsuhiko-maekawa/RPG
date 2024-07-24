using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.Entity
{
    public class FrameEntity
    {
        public FrameNumber FrameNumber { get; }
        public ImmutableList<ControllerCode> ControllerCodeList { get; }
        public ImmutableList<UseCaseCode> BusinessLogicCodeList { get; }
        public ImmutableList<OutputCode> PresenterCodeList { get; }
        
        public FrameEntity(
            FrameNumber frameNumber,
            IList<ControllerCode> controllerCodeList = null,
            IList<UseCaseCode> businessLogicCodeList = null,
            IList<OutputCode> presenterCodeList = null)
        {
            FrameNumber = frameNumber;
            ControllerCodeList = controllerCodeList?.ToImmutableList() ?? ImmutableList<ControllerCode>.Empty;
            BusinessLogicCodeList = businessLogicCodeList?.ToImmutableList() ?? ImmutableList<UseCaseCode>.Empty;
            PresenterCodeList = presenterCodeList?.ToImmutableList() ?? ImmutableList<OutputCode>.Empty;
        }

        public FrameEntity Merge(FrameEntity frameEntity)
        {
            if (!Equals(frameEntity, this)) throw new InvalidOperationException();
            var newControllerCodeList = ControllerCodeList.AddRange(frameEntity.ControllerCodeList);
            var newBusinessLogicCodeList = BusinessLogicCodeList.AddRange(frameEntity.BusinessLogicCodeList);
            var newPresenterCodeList = PresenterCodeList.AddRange(frameEntity.PresenterCodeList);
            return new FrameEntity(
                frameNumber: FrameNumber,
                controllerCodeList: newControllerCodeList,
                businessLogicCodeList: newBusinessLogicCodeList,
                presenterCodeList: newPresenterCodeList);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var frameEntity = (FrameEntity)obj;
            return Equals(FrameNumber, frameEntity.FrameNumber);
        }

        public override int GetHashCode()
        {
            return FrameNumber.GetHashCode();
        }
    }
}