using System;
using System.Collections.Generic;
using System.Linq;

public class TimeReverseController : MonoSingleton<TimeReverseController>
{
    public static event Action<int> OnFrameUpdateAction;
    
    private List<TimeRecorder> recorders;

    private int recordedFrames;
    private float rewindFrames;

    public float RewindSpeed => 2f;

    private bool areFramesRecording;
    private bool isRewinding;
    
    private void Awake() => FillRecorders();

    private void FillRecorders() => recorders = GetComponentsInChildren<TimeRecorder>().ToList();

    private void Update()
    {
        RecordFrames();
        RewindFrames();
    }

    private void RecordFrames()
    {
        if (areFramesRecording)
            IncreaseFrames();
    }

    private void IncreaseFrames()
    {
        recordedFrames++;
        OnFrameUpdateAction?.Invoke(recordedFrames);
    }

    private void RewindFrames()
    {
        if (!isRewinding) return;
        
        RestoredFrames();
        DecreaseFramesToRewindBySpeed();
    }

    private void RestoredFrames()
    {
        foreach (TimeRecorder recorder in recorders)
        {
            int frames = (int)(rewindFrames - 1);
            recorder.RestoreFrame(frames);
        }
    }

    private void DecreaseFramesToRewindBySpeed() => rewindFrames -= RewindSpeed;

    #region UI Calls

    public void StartRecording()
    {
        recorders.ForEach(x => x.StartRecording());
        ResetRecordingStatus();
    }

    private void ResetRecordingStatus()
    {
        areFramesRecording = true;
        rewindFrames = 0;
        recordedFrames = 0;
    }
    
    public void StopRecording()
    {
        recorders.ForEach(x => x.StopRecording());
        areFramesRecording = false;
    }

    public void StartRewind()
    {
        isRewinding = true;

        rewindFrames = recordedFrames;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }

    #endregion
}
