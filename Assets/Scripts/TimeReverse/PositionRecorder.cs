using System.Collections.Generic;
using UnityEngine;

public class PositionRecorder : TimeRecorder
{
    public List<Vector3> recorderPositions = new();

    public override bool Finished { get; protected set; }

    public override void RestoreFrame(int frame)
    {
        Debug.LogWarning($"Count: {recorderPositions.Count}");
        if (frame >= 0)
            transform.position = recorderPositions[frame];
        else
            StopRestoring();
    }

    protected override void StopRestoring() => Finished = true;

    public override void StartRecording()
    {
        recorderPositions.Clear();
        Finished = false;
    }

    public override void StopRecording()
    {
        
    }

    protected override void Record()
    {
        if (TimeReverseController.Instance.IsRecording)
            recorderPositions.Add(transform.position);
    }
}
