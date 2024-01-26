using System.Collections.Generic;
using UnityEngine;

namespace TimeReverse.SpecifiedRecorders
{
    public class PositionRecorder : TimeRecorder
    {
        private readonly List<Vector3> recordedPositions = new();

        protected override void InitializeAction() {}

        protected override void StartRecordingAction() => recordedPositions.Clear();
        protected override void RecordingAction() => recordedPositions.Add(transform.position);
        protected override void StopRecordingAction() {}
        
        protected override void StartRewindAction() {}
        protected override void RewindAction(int frame) => transform.position = recordedPositions[frame];
        protected override void StopRewindAction() {}
    }
}
