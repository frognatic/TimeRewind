using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeReverseController : MonoBehaviour
{
    public static TimeReverseController Instance;
    
    private List<TimeRecorder> recorders;

    private float rewindSpeed = 0.25f;
    
    private int savedFrames;
    private float recordedFrames;
    
    public bool IsRecording { get; private set; }
    public bool IsPlaying { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        FillRecorders();
    }

    private void FillRecorders() => recorders = GetComponentsInChildren<TimeRecorder>().ToList();

    public void StartRecording()
    {
        savedFrames = 0;
        IsRecording = true;
        recorders.ForEach(x => x.StartRecording());
    }

    public void StopRecording()
    {
        IsRecording = false;
        recordedFrames = savedFrames;
        
        recorders.ForEach(x => x.StopRecording());
    }
    
    public void StartPlaying()
    {
        IsPlaying = true;
    }

    public void StopPlaying()
    {
        IsPlaying = false;
    }

    private void Update()
    {
        IncreaseFramesCounter();
        ReplayFrames();
    }

    private void IncreaseFramesCounter()
    {
        if (IsRecording) 
            savedFrames++;
    }
    
    private void ReplayFrames()
    {
        if (IsPlaying) 
            RestoreFrames();
    }
    
    private void RestoreFrames()
    {
        if (AreRecordsFinished) return;
        
        foreach (TimeRecorder recorder in recorders)
        {
            int frames = (int)(recordedFrames - 1);
            Debug.LogWarning($"Frames: {frames} finished? {AreRecordsFinished}");
            recorder.RestoreFrame(frames);
        }

        Debug.LogWarning($"Decrease frames");
        recordedFrames -= rewindSpeed;
    }

    private bool AreRecordsFinished => recorders.TrueForAll(x => x.Finished);
}
