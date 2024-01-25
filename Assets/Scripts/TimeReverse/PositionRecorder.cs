using System.Collections.Generic;
using UnityEngine;

public class PositionRecorder : TimeRecorder
{
    public List<Vector3> recorderPositions = new();
    
    protected override void RestoreAction(int frame) => transform.position = recorderPositions[frame];

    protected override void StartRecordingAction() => recorderPositions.Clear();

    protected override void RecordingAction() => recorderPositions.Add(transform.position);
}
