using UnityEngine;

namespace TimeReverse.SpecifiedRecorders
{
    public class PositionRecorder : TimeRecorder
    {
        private readonly FrameContainer<Vector3> positionsRecorder = new();

        protected override void InitializeAction() {}

        protected override void StartRecordingAction() => positionsRecorder.Reset();

        protected override void RecordingAction(int frame) => positionsRecorder.Record(frame, transform.position);

        protected override void StopRecordingAction() {}
        
        protected override void StartRewindAction() => positionsRecorder.StartRewindAction();
        protected override void RewindAction(int frame, bool frameByFrame)
        {
            Vector3 getRewind = positionsRecorder.GetRewind(frame, frameByFrame);
            transform.position = getRewind;
        }

        protected override void StopRewindAction() {}
    }
}
